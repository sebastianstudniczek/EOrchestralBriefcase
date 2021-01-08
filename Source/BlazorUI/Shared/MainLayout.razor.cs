using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EOrchestralBriefcase.BlazorUI.Services;
using Microsoft.AspNetCore.Components;

namespace EOrchestralBriefcase.BlazorUI.Shared
{
    public partial class MainLayout 
    {
        [Inject]
        public IOrchestralBriefcasesService OrchestralBriefcaseseService { get; set; }

        public IEnumerable<OrchestralBriefcasesService> OrchestralBriefcases { get; set; }
    }
}
