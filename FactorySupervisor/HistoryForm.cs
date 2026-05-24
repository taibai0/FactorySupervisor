using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
///
/// 历史查询都是查询最近十分钟的数据
///
namespace FactorySupervisor
{
    public partial class HistoryForm : Form
    {
        private readonly ITagHistoryRepository _tagHistoryRepository = new SqlTagHistoryRepository();
        private readonly IAuditLogRepository _auditLogRepository = new SqlAuditLogRepository();
        private readonly IAlarmHistoryRepository _alarmHistoryRepository = new SqlAlarmHistoryRepository();


        public HistoryForm()
        {
            InitializeComponent();

            dgvHistory.AutoGenerateColumns = true;
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                      
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {

        }

        //历史点位数据查询
        private async void btnQueryTagHistory_Click(object sender, EventArgs e)
        {
            var to = DateTimeOffset.Now;
            var from = to.AddMinutes(-10);

            var values = await _tagHistoryRepository.QueryAsync(from, to);

            var rows = values.Select(v => new
            {
                v.TagId,
                v.DeviceId,
                Value = v.Value?.ToString() ?? "",
                Quality = v.Quality.ToString(),
                Time = v.Timestamp.ToString("HH:mm:ss"),
                Error = v.Error ?? ""
            }).ToList();

            dgvHistory.DataSource = null;
            dgvHistory.DataSource = rows;
        }

        //历史报警查询
        private async void btnQueryAlarmHistory_Click(object sender, EventArgs e)
        {
            var to=DateTimeOffset.Now;
            var from = to.AddMinutes(-10);

            var alarms = await _alarmHistoryRepository.QueryAsync(from, to);
            
            var rows = alarms.Select(a => new
            {
                a.RuleName,
                Level = a.Level.ToString(),
                State = a.State.ToString(),
                TriggerValue = a.TriggerValue?.ToString() ?? "",
                TriggerTime = a.TriggerTime.ToString("HH:mm:ss"),
                RecoverTime = a.RecoverTime?.ToString("HH:mm:ss") ?? ""
            }).ToList();

            dgvHistory.DataSource = null;
            dgvHistory.DataSource = rows;
        }

        //审计日志查询
        private async void btnQueryAuditLog_Click(object sender, EventArgs e)
        {
            var to= DateTimeOffset.Now;
            var from = to.AddMinutes(-10);

            var logs=await _auditLogRepository.QueryAsync(from,to);

            var rows = logs.Select(x => new
            {
                x.Username,
                x.Action,
                x.Detail,
                Time=x.CreatedAt.ToString("HH:mm:ss")
            }).ToList();

            dgvHistory.DataSource = null;
            dgvHistory.DataSource = rows;
        }
    }
}
