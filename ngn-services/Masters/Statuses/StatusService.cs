using Microsoft.EntityFrameworkCore;
using ngn_data;
using ngn_DbModels.Models;
using ngn_services.Masters;

namespace ngn_services.Masters.Statuses;

public sealed class StatusService(AppDbContext db) : IStatusService
{
    public async Task<IReadOnlyList<StatusResponse>> GetStatusesAsync(bool includeInactive, CancellationToken ct)
        {
            var q = db.StatusMasters.AsNoTracking();
            if (!includeInactive) q = q.Where(x => x.IsActive);
            var rows = await q.OrderBy(x => x.StatusName).ToListAsync(ct);
            return rows.Select(StatusDto).ToList();
        }

    public async Task<StatusResponse?> GetStatusAsync(int id, CancellationToken ct)
    {
        var x = await db.StatusMasters.AsNoTracking().SingleOrDefaultAsync(x => x.StatusMasterId == id, ct);
        return x is null ? null : StatusDto(x);
    }

    public async Task<MasterOperationResult<StatusResponse>> CreateStatusAsync(StatusRequest r, CancellationToken ct)
        {
            var code = r.StatusCode.Trim();
            if (await db.StatusMasters.AnyAsync(x => x.StatusCode == code, ct)) return new(null, Conflict: true);
            var e = new StatusMaster { StatusCode = code, StatusName = r.StatusName.Trim(), ManagementType = r.ManagementType?.Trim(), IsTerminalFinalStatus = r.IsTerminalFinalStatus, BadgeVisualStyle = r.BadgeVisualStyle?.Trim(), Remarks = r.Remarks?.Trim(), IsActive = true };
            db.StatusMasters.Add(e); await db.SaveChangesAsync(ct); return new(StatusDto(e));
        }

    public async Task<MasterOperationResult<StatusResponse>> UpdateStatusAsync(int id, StatusRequest r, CancellationToken ct)
        {
            var e = await db.StatusMasters.SingleOrDefaultAsync(x => x.StatusMasterId == id, ct);
            if (e is null) return new(null, NotFound: true);
            var code = r.StatusCode.Trim();
            if (await db.StatusMasters.AnyAsync(x => x.StatusMasterId != id && x.StatusCode == code, ct)) return new(null, Conflict: true);
            e.StatusCode = code; e.StatusName = r.StatusName.Trim(); e.ManagementType = r.ManagementType?.Trim(); e.IsTerminalFinalStatus = r.IsTerminalFinalStatus; e.BadgeVisualStyle = r.BadgeVisualStyle?.Trim(); e.Remarks = r.Remarks?.Trim();
            await db.SaveChangesAsync(ct); return new(StatusDto(e));
        }

    public async Task<MasterOperationResult<StatusResponse>> SetStatusActiveAsync(int id, bool isActive, CancellationToken ct)
        {
            var e = await db.StatusMasters.SingleOrDefaultAsync(x => x.StatusMasterId == id, ct);
            if (e is null) return new(null, NotFound: true);
            e.IsActive = isActive; await db.SaveChangesAsync(ct); return new(StatusDto(e));
        }

    public async Task<MasterOperationResult<StatusResponse>> DeleteStatusAsync(int id, CancellationToken ct) => await SetStatusActiveAsync(id, false, ct);

    private static StatusResponse StatusDto(StatusMaster x) => new(x.StatusMasterId, x.StatusCode, x.StatusName, x.ManagementType, x.IsTerminalFinalStatus, x.BadgeVisualStyle, x.Remarks, x.IsActive);
}
