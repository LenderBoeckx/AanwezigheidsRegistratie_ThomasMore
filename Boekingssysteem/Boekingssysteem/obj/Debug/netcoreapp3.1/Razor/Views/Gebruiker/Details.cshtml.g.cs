#pragma checksum "C:\Users\lende\Documents\thomasmore\jaar 2\projecten van het werkveld\project\Boekingssysteem\Boekingssysteem\Views\Gebruiker\Details.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "31f3bc70b87c552e12daf7a631f2d9a06674d6ce160da02d031674f127ffd582"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCoreGeneratedDocument.Views_Gebruiker_Details), @"mvc.1.0.view", @"/Views/Gebruiker/Details.cshtml")]
namespace AspNetCoreGeneratedDocument
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\lende\Documents\thomasmore\jaar 2\projecten van het werkveld\project\Boekingssysteem\Boekingssysteem\Views\_ViewImports.cshtml"
using Boekingssysteem

#nullable disable
    ;
#nullable restore
#line 2 "C:\Users\lende\Documents\thomasmore\jaar 2\projecten van het werkveld\project\Boekingssysteem\Boekingssysteem\Views\_ViewImports.cshtml"
using Boekingssysteem.Models

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"31f3bc70b87c552e12daf7a631f2d9a06674d6ce160da02d031674f127ffd582", @"/Views/Gebruiker/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"cca80d9c53d90326a595c411b2f88c91f31c969804f93583293dae2e64aa34e5", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    internal sealed class Views_Gebruiker_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<
#nullable restore
#line 1 "C:\Users\lende\Documents\thomasmore\jaar 2\projecten van het werkveld\project\Boekingssysteem\Boekingssysteem\Views\Gebruiker\Details.cshtml"
       Boekingssysteem.ViewModels.GebruikerDetailsViewModel

#line default
#line hidden
#nullable disable
    >
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary mb-5 mt-5"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\lende\Documents\thomasmore\jaar 2\projecten van het werkveld\project\Boekingssysteem\Boekingssysteem\Views\Gebruiker\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable

            WriteLiteral("\r\n<h1>Gebruiker informatie</h1>\r\n\r\n<p> Personeelnummer: ");
            Write(
#nullable restore
#line 9 "C:\Users\lende\Documents\thomasmore\jaar 2\projecten van het werkveld\project\Boekingssysteem\Boekingssysteem\Views\Gebruiker\Details.cshtml"
                      Model.Personeelnummer

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</p>\r\n<p> Naam: ");
            Write(
#nullable restore
#line 10 "C:\Users\lende\Documents\thomasmore\jaar 2\projecten van het werkveld\project\Boekingssysteem\Boekingssysteem\Views\Gebruiker\Details.cshtml"
           Model.Naam

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</p>\r\n<p> Voornaam: ");
            Write(
#nullable restore
#line 11 "C:\Users\lende\Documents\thomasmore\jaar 2\projecten van het werkveld\project\Boekingssysteem\Boekingssysteem\Views\Gebruiker\Details.cshtml"
               Model.Voornaam

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</p>\r\n<p> Email: ");
            Write(
#nullable restore
#line 12 "C:\Users\lende\Documents\thomasmore\jaar 2\projecten van het werkveld\project\Boekingssysteem\Boekingssysteem\Views\Gebruiker\Details.cshtml"
            Model.UserName

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</p>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "31f3bc70b87c552e12daf7a631f2d9a06674d6ce160da02d031674f127ffd5826134", async() => {
                WriteLiteral("Terug");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Boekingssysteem.ViewModels.GebruikerDetailsViewModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
