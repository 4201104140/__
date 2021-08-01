using RulesEngine.Models;
using RulesEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    public class Demo
    {
        public void Run()
        {
            var workflows = GetWorkflowRules();
            var re = new RulesEngine.RulesEngine(workflows, )
        }

        private WorkflowRules[] GetWorkflowRules()
        {
            return new WorkflowRules[] {
                new WorkflowRules {
                    WorkflowName = "successReturnContextAction",
                    Rules = new Rule[] {
                        new Rule {
                            RuleName = "trueRule",
                            Expression = "input1 == true",
                            Actions = new RuleActions() {
                                OnSuccess = new ActionInfo {
                                    Name = "ReturnContext",
                                    Context =  new Dictionary<string, object> {
                                        {"stringContext", "hello"},
                                        {"intContext",1 },
                                        {"objectContext", new { a = "hello", b = 123 } }
                                    }
                                }

                            }

                        },


                    }
                }

            };
        }
    }
}
