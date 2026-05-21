using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Application.Services
{
    public sealed class AlarmEvaluationService
    {
        private readonly IAlarmRuleRepository _ruleRepository;
        private readonly ITagValueCache _tagValueCache;
        private readonly IAlarmStateStore _alarmStateStore;

        public AlarmEvaluationService(
            IAlarmRuleRepository ruleRepository,
            ITagValueCache tagValueCache,
            IAlarmStateStore alarmStateStore)
        {
            _ruleRepository = ruleRepository;
            _tagValueCache = tagValueCache;
            _alarmStateStore = alarmStateStore;
        }
        
        public async Task RunAsync(CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    await EvaluateOnceAsync(ct);
                    await Task.Delay(1000, ct);
                }
            }
            catch (OperationCanceledException)
            {
                // 正常停止
            }
        }

        private async Task EvaluateOnceAsync(CancellationToken ct)
        {
            var rules = await _ruleRepository.GetEnableRulesAsync(ct);

            foreach(var rule in rules)
            {
                if (!_tagValueCache.TryGet(rule.TagId, out var tagValue) || tagValue is null)
                    continue;

                if (tagValue.Quality != Domain.Enums.TagQuality.Good)
                    continue;

                if (!TryConvertToDouble(tagValue.Value, out var currentValue))
                    continue;

                

                var triggered = IsTriggered(currentValue, rule);

                if (triggered)
                {
                    if (!_alarmStateStore.HasActive(rule.Id))
                    {
                        var alarm = new AlarmRecord
                        {
                            RuleId = rule.Id,
                            TagId = rule.TagId,
                            RuleName = rule.Name,
                            Level = rule.Level,
                            TriggerValue = tagValue.Value,
                            TriggerTime = DateTimeOffset.Now
                        };
                      

                        _alarmStateStore.UpsertActive(alarm);
                    }
                }
                else
                {
                    if (_alarmStateStore.HasActive(rule.Id))
                    {
                        _alarmStateStore.Recover(rule.Id, DateTimeOffset.Now);
                    }
                }

            }
        }

        private bool IsTriggered(double currentValue, AlarmRule rule)
        {
            return rule.ConditionType switch
            {
                AlarmConditionType.GreaterThan => currentValue > rule.Threshold,
                AlarmConditionType.GreaterThanOrEqual => currentValue >= rule.Threshold,
                AlarmConditionType.LessThan => currentValue < rule.Threshold,
                AlarmConditionType.LessThanOrEqual => currentValue <= rule.Threshold,
                AlarmConditionType.Equal => Math.Abs(currentValue - rule.Threshold) < 0.000001,
                AlarmConditionType.NotEqual => Math.Abs(currentValue - rule.Threshold) >= 0.000001,
                _ => false
            };
        }

        private bool TryConvertToDouble(object? value, out double result)
        {
            if(value is null)
            {
                result = 0;
                return false;
            }
            try
            {
                result = Convert.ToDouble(value);
                return true;
            }
            catch
            {
                result = 0;
                return false;
            }
        }
    }
}
