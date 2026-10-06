using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ngn_data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallTypeMaster",
                columns: table => new
                {
                    CallTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CallName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallTypeMaster", x => x.CallTypeId);
                });

            migrationBuilder.CreateTable(
                name: "CaseTechnicalComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolutionId = table.Column<int>(type: "int", nullable: false),
                    TechnicalPersonName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TechnicalPersonNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TechnicalComments = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseTechnicalComments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CircleByZones",
                columns: table => new
                {
                    CircleZoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Zone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CircleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CircleByZones", x => x.CircleZoneId);
                });

            migrationBuilder.CreateTable(
                name: "CircleMaster",
                columns: table => new
                {
                    CircleCityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CircleCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CircleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CityCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CircleMaster", x => x.CircleCityId);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    ContactUsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Circle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmailId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.ContactUsId);
                });

            migrationBuilder.CreateTable(
                name: "FlashNewsNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TargetAudience = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TargetCircle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsTicker = table.Column<bool>(type: "bit", nullable: false),
                    IsModalAlert = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashNewsNotification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterDatacontact",
                columns: table => new
                {
                    ContactID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterDatacontact", x => x.ContactID);
                });

            migrationBuilder.CreateTable(
                name: "ProjectGroups",
                columns: table => new
                {
                    ProjectGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectGroupName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProjectGroupDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectGroups", x => x.ProjectGroupId);
                });

            migrationBuilder.CreateTable(
                name: "SeverityMasters",
                columns: table => new
                {
                    SeverityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeverityCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SeverityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Level = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ResponseSLA = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    ResolutionSLA = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeverityMasters", x => x.SeverityId);
                });

            migrationBuilder.CreateTable(
                name: "Solutions",
                columns: table => new
                {
                    SolutionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProblemType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RMANo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SRNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Resolution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnicalPersonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnicalPersonNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnicalPersonComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CallStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeverityId = table.Column<int>(type: "int", nullable: false),
                    ClosedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    Minutes = table.Column<int>(type: "int", nullable: false),
                    Ageing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BSNLCallStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BSNLLoginName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BSNLClosedDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BSNLClosedHours = table.Column<int>(type: "int", nullable: false),
                    BSNLClosedMinutes = table.Column<int>(type: "int", nullable: false),
                    ResponseTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResolutionTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solutions", x => x.SolutionId);
                });

            migrationBuilder.CreateTable(
                name: "StatusMasters",
                columns: table => new
                {
                    StatusMasterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ManagementType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsTerminalFinalStatus = table.Column<bool>(type: "bit", nullable: false),
                    BadgeVisualStyle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusMasters", x => x.StatusMasterId);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMasters",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProjectGroupId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMasters", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_ProjectMasters_ProjectGroups_ProjectGroupId",
                        column: x => x.ProjectGroupId,
                        principalTable: "ProjectGroups",
                        principalColumn: "ProjectGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EquipmentDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.EquipmentId);
                    table.ForeignKey(
                        name: "FK_Equipment_ProjectMasters_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectMasters",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MasterData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    CircleCityId = table.Column<int>(type: "int", nullable: false),
                    SSA = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SiteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ExchangeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SiteAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    EquipmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pincode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    BSNLContactId = table.Column<int>(type: "int", nullable: true),
                    HCLContactId = table.Column<int>(type: "int", nullable: true),
                    UTContactId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterData_CircleMaster_CircleCityId",
                        column: x => x.CircleCityId,
                        principalTable: "CircleMaster",
                        principalColumn: "CircleCityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterData_MasterDatacontact_BSNLContactId",
                        column: x => x.BSNLContactId,
                        principalTable: "MasterDatacontact",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterData_MasterDatacontact_HCLContactId",
                        column: x => x.HCLContactId,
                        principalTable: "MasterDatacontact",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterData_MasterDatacontact_UTContactId",
                        column: x => x.UTContactId,
                        principalTable: "MasterDatacontact",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterData_ProjectMasters_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectMasters",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEmailFeatureMappings",
                columns: table => new
                {
                    ProjectEmailFeatureMappingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsCallLogged = table.Column<bool>(type: "bit", nullable: false),
                    IsSLAEscalation = table.Column<bool>(type: "bit", nullable: false),
                    IsTicketResolvedClosed = table.Column<bool>(type: "bit", nullable: false),
                    IsCallAssignment = table.Column<bool>(type: "bit", nullable: false),
                    IsRMAInitiated = table.Column<bool>(type: "bit", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEmailFeatureMappings", x => x.ProjectEmailFeatureMappingId);
                    table.ForeignKey(
                        name: "FK_ProjectEmailFeatureMappings_ProjectMasters_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectMasters",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEmails",
                columns: table => new
                {
                    ProjectEmailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToEmailAddress = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    CCEmailAddress = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsCustomerEmailOverride = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEmails", x => x.ProjectEmailId);
                    table.ForeignKey(
                        name: "FK_ProjectEmails_ProjectMasters_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectMasters",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectItemDetails",
                columns: table => new
                {
                    ProjectItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SubType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EquipmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ItemDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PartNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectItemDetails", x => x.ProjectItemId);
                    table.ForeignKey(
                        name: "FK_ProjectItemDetails_ProjectMasters_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectMasters",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CallRegisterForOthers",
                columns: table => new
                {
                    CaseId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    BSNLContactName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BSNLContactNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LandlineNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CallType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NatureOfProblenId = table.Column<int>(type: "int", nullable: false),
                    ProblemDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LoginName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoginTimeHours = table.Column<int>(type: "int", nullable: false),
                    LoginTimeMinutes = table.Column<int>(type: "int", nullable: false),
                    PartNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SiteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    SeverityId = table.Column<int>(type: "int", nullable: false),
                    DocketStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallRegisterForOthers", x => x.CaseId);
                    table.ForeignKey(
                        name: "FK_CallRegisterForOthers_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CallRegisterForOthers_ProjectMasters_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectMasters",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CallRegisterForOthers_StatusMasters_DocketStatusId",
                        column: x => x.DocketStatusId,
                        principalTable: "StatusMasters",
                        principalColumn: "StatusMasterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentPartNumbers",
                columns: table => new
                {
                    EquipmentPartNumberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentPartNumbers", x => x.EquipmentPartNumberId);
                    table.ForeignKey(
                        name: "FK_EquipmentPartNumbers_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NatureOfProblem",
                columns: table => new
                {
                    NatureOfProblemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NatureOfProblemName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProjectItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NatureOfProblem", x => x.NatureOfProblemId);
                    table.ForeignKey(
                        name: "FK_NatureOfProblem_ProjectItemDetails_ProjectItemId",
                        column: x => x.ProjectItemId,
                        principalTable: "ProjectItemDetails",
                        principalColumn: "ProjectItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CallRegister",
                columns: table => new
                {
                    CaseId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    BSNLContactName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BSNLContactNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LandlineNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CallType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NatureOfProblemId = table.Column<int>(type: "int", nullable: false),
                    ProblemDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LoginName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoginTimeHours = table.Column<int>(type: "int", nullable: false),
                    LoginTimeMinutes = table.Column<int>(type: "int", nullable: false),
                    PartNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SiteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    SeverityId = table.Column<int>(type: "int", nullable: false),
                    DocketStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallRegister", x => x.CaseId);
                    table.ForeignKey(
                        name: "FK_CallRegister_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CallRegister_NatureOfProblem_NatureOfProblemId",
                        column: x => x.NatureOfProblemId,
                        principalTable: "NatureOfProblem",
                        principalColumn: "NatureOfProblemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CallRegister_ProjectMasters_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectMasters",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CallRegister_SeverityMasters_SeverityId",
                        column: x => x.SeverityId,
                        principalTable: "SeverityMasters",
                        principalColumn: "SeverityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CallRegister_StatusMasters_DocketStatusId",
                        column: x => x.DocketStatusId,
                        principalTable: "StatusMasters",
                        principalColumn: "StatusMasterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CallRegister_DocketStatusId",
                table: "CallRegister",
                column: "DocketStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CallRegister_EquipmentId",
                table: "CallRegister",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CallRegister_NatureOfProblemId",
                table: "CallRegister",
                column: "NatureOfProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_CallRegister_ProjectId",
                table: "CallRegister",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CallRegister_SeverityId",
                table: "CallRegister",
                column: "SeverityId");

            migrationBuilder.CreateIndex(
                name: "IX_CallRegisterForOthers_DocketStatusId",
                table: "CallRegisterForOthers",
                column: "DocketStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CallRegisterForOthers_EquipmentId",
                table: "CallRegisterForOthers",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CallRegisterForOthers_ProjectId",
                table: "CallRegisterForOthers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CircleByZones_Zone_CircleName",
                table: "CircleByZones",
                columns: new[] { "Zone", "CircleName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CircleMaster_CircleCode_CityCode",
                table: "CircleMaster",
                columns: new[] { "CircleCode", "CityCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CircleMaster_CircleName",
                table: "CircleMaster",
                column: "CircleName");

            migrationBuilder.CreateIndex(
                name: "IX_CircleMaster_CityName",
                table: "CircleMaster",
                column: "CityName");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_ProjectId_EquipmentName",
                table: "Equipment",
                columns: new[] { "ProjectId", "EquipmentName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPartNumbers_EquipmentId_IsActive",
                table: "EquipmentPartNumbers",
                columns: new[] { "EquipmentId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPartNumbers_EquipmentId_PartNumber",
                table: "EquipmentPartNumbers",
                columns: new[] { "EquipmentId", "PartNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlashNewsNotification_Status_StartDate_EndDate",
                table: "FlashNewsNotification",
                columns: new[] { "Status", "StartDate", "EndDate" });

            migrationBuilder.CreateIndex(
                name: "IX_MasterData_BSNLContactId",
                table: "MasterData",
                column: "BSNLContactId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterData_CircleCityId",
                table: "MasterData",
                column: "CircleCityId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterData_HCLContactId",
                table: "MasterData",
                column: "HCLContactId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterData_ProjectId_CircleCityId",
                table: "MasterData",
                columns: new[] { "ProjectId", "CircleCityId" });

            migrationBuilder.CreateIndex(
                name: "IX_MasterData_UTContactId",
                table: "MasterData",
                column: "UTContactId");

            migrationBuilder.CreateIndex(
                name: "IX_NatureOfProblem_ProjectItemId_NatureOfProblemName",
                table: "NatureOfProblem",
                columns: new[] { "ProjectItemId", "NatureOfProblemName" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEmailFeatureMappings_ProjectId",
                table: "ProjectEmailFeatureMappings",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEmails_ProjectId_IsActive",
                table: "ProjectEmails",
                columns: new[] { "ProjectId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectGroups_ProjectGroupName",
                table: "ProjectGroups",
                column: "ProjectGroupName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectItemDetails_ProjectId_ItemName",
                table: "ProjectItemDetails",
                columns: new[] { "ProjectId", "ItemName" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMasters_ProjectGroupId",
                table: "ProjectMasters",
                column: "ProjectGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMasters_ProjectName",
                table: "ProjectMasters",
                column: "ProjectName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeverityMasters_SeverityCode",
                table: "SeverityMasters",
                column: "SeverityCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatusMasters_IsActive",
                table: "StatusMasters",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_StatusMasters_StatusCode",
                table: "StatusMasters",
                column: "StatusCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallRegister");

            migrationBuilder.DropTable(
                name: "CallRegisterForOthers");

            migrationBuilder.DropTable(
                name: "CallTypeMaster");

            migrationBuilder.DropTable(
                name: "CaseTechnicalComments");

            migrationBuilder.DropTable(
                name: "CircleByZones");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "EquipmentPartNumbers");

            migrationBuilder.DropTable(
                name: "FlashNewsNotification");

            migrationBuilder.DropTable(
                name: "MasterData");

            migrationBuilder.DropTable(
                name: "ProjectEmailFeatureMappings");

            migrationBuilder.DropTable(
                name: "ProjectEmails");

            migrationBuilder.DropTable(
                name: "Solutions");

            migrationBuilder.DropTable(
                name: "NatureOfProblem");

            migrationBuilder.DropTable(
                name: "SeverityMasters");

            migrationBuilder.DropTable(
                name: "StatusMasters");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "CircleMaster");

            migrationBuilder.DropTable(
                name: "MasterDatacontact");

            migrationBuilder.DropTable(
                name: "ProjectItemDetails");

            migrationBuilder.DropTable(
                name: "ProjectMasters");

            migrationBuilder.DropTable(
                name: "ProjectGroups");
        }
    }
}
