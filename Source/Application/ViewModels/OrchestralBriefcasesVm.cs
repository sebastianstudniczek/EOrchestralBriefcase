using System.Collections.Generic;

namespace EOrchestralBriefcase.Application.ViewModels
{
    public class OrchestralBriefcasesVm
    {
        public IList<OrchestralBriefcaseVm> OrchestralBriefcases { get; set; }
        public int Count { get; set; }
    }
}
