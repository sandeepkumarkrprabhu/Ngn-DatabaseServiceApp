/*
    EquipmentPartNumber migration

    Safe migration:
    1. Creates the child table only when it does not exist.
    2. Does not modify or delete existing EquipmentMaster/ProjectItemDetails data.
    3. Copies existing PartNumber values from ProjectItemDetails only when
       an EquipmentMaster row can be matched by ProjectId + EquipmentName.
    4. Duplicate equipment/part-number pairs are ignored.
    5. Existing source data remains untouched.

    IMPORTANT:
    Review the validation query before executing this script against production.
    The ProjectItemDetails -> EquipmentMaster mapping assumes that
    ProjectItemDetails.ProjectId maps to EquipmentMaster.ProjectId and
    ProjectItemDetails.EquipmentName maps to EquipmentMaster.EquipmentName.
*/

IF OBJECT_ID(N'dbo.EquipmentPartNumber', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.EquipmentPartNumber
    (
        EquipmentPartNumberId INT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_EquipmentPartNumber PRIMARY KEY,
        EquipmentId INT NOT NULL,
        PartNumber NVARCHAR(50) NOT NULL,
        IsActive BIT NOT NULL
            CONSTRAINT DF_EquipmentPartNumber_IsActive DEFAULT (1),
        CreatedDate DATETIME2 NOT NULL
            CONSTRAINT DF_EquipmentPartNumber_CreatedDate DEFAULT (SYSUTCDATETIME()),
        UpdatedDate DATETIME2 NOT NULL
            CONSTRAINT DF_EquipmentPartNumber_UpdatedDate DEFAULT (SYSUTCDATETIME()),

        CONSTRAINT FK_EquipmentPartNumber_EquipmentMaster
            FOREIGN KEY (EquipmentId)
            REFERENCES dbo.EquipmentMaster (EquipmentId)
            ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = N'UX_EquipmentPartNumber_EquipmentId_PartNumber'
      AND object_id = OBJECT_ID(N'dbo.EquipmentPartNumber')
)
BEGIN
    CREATE UNIQUE INDEX UX_EquipmentPartNumber_EquipmentId_PartNumber
        ON dbo.EquipmentPartNumber (EquipmentId, PartNumber);
END;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = N'IX_EquipmentPartNumber_EquipmentId_IsActive'
      AND object_id = OBJECT_ID(N'dbo.EquipmentPartNumber')
)
BEGIN
    CREATE INDEX IX_EquipmentPartNumber_EquipmentId_IsActive
        ON dbo.EquipmentPartNumber (EquipmentId, IsActive);
END;
GO

/*
    Data migration.

    Only non-empty PartNumber values are migrated.
    The EXISTS condition prevents duplicate inserts when the script is rerun.
*/
INSERT INTO dbo.EquipmentPartNumber
(
    EquipmentId,
    PartNumber,
    IsActive,
    CreatedDate,
    UpdatedDate
)
SELECT DISTINCT
    e.EquipmentId,
    LTRIM(RTRIM(p.PartNumber)),
    1,
    SYSUTCDATETIME(),
    SYSUTCDATETIME()
FROM dbo.ProjectItemDetails p
INNER JOIN dbo.EquipmentMaster e
    ON e.ProjectId = p.ProjectId
   AND e.EquipmentName = p.EquipmentName
WHERE NULLIF(LTRIM(RTRIM(p.PartNumber)), N'') IS NOT NULL
  AND NOT EXISTS
  (
      SELECT 1
      FROM dbo.EquipmentPartNumber ep
      WHERE ep.EquipmentId = e.EquipmentId
        AND ep.PartNumber = LTRIM(RTRIM(p.PartNumber))
  );
GO

/*
    Validation: source rows that could not be mapped.
*/
SELECT
    p.ProjectItemId,
    p.ProjectId,
    p.EquipmentName,
    p.PartNumber
FROM dbo.ProjectItemDetails p
LEFT JOIN dbo.EquipmentMaster e
    ON e.ProjectId = p.ProjectId
   AND e.EquipmentName = p.EquipmentName
WHERE NULLIF(LTRIM(RTRIM(p.PartNumber)), N'') IS NOT NULL
  AND e.EquipmentId IS NULL;
GO

/*
    Validation: migrated counts.
*/
SELECT
    e.EquipmentId,
    e.EquipmentName,
    COUNT(ep.EquipmentPartNumberId) AS PartNumberCount
FROM dbo.EquipmentMaster e
LEFT JOIN dbo.EquipmentPartNumber ep
    ON ep.EquipmentId = e.EquipmentId
GROUP BY e.EquipmentId, e.EquipmentName
ORDER BY e.EquipmentId;
GO
