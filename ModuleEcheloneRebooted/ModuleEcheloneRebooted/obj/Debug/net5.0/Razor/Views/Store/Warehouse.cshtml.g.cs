#pragma checksum "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/Store/Warehouse.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3e7400c45949269352094efd16fa6039e13d05d2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(ModuleEcheloneRebooted.Models.Store.Views_Store_Warehouse), @"mvc.1.0.view", @"/Views/Store/Warehouse.cshtml")]
namespace ModuleEcheloneRebooted.Models.Store
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/_ViewImports.cshtml"
using ModuleEcheloneRebooted;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/_ViewImports.cshtml"
using ModuleEcheloneRebooted.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/Store/Warehouse.cshtml"
using Microsoft.AspNetCore.Mvc.Rendering;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3e7400c45949269352094efd16fa6039e13d05d2", @"/Views/Store/Warehouse.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0c4aaa5042671c1feb9b4c0bb71385b3db9540f3", @"/Views/_ViewImports.cshtml")]
    public class Views_Store_Warehouse : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ModuleEcheloneRebooted.Models.ProductModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ProductDetails", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\n");
#nullable restore
#line 4 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/Store/Warehouse.cshtml"
  
    ViewBag.Title = "WareHouse";
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<div style=\"text-align: center;\" >\n    <h1>Warehouse</h1>\n    <hr/>\n</div>\n<div class=\"align-content-center\" style=\n     \"text-align: center; max-width:80%; margin-left: auto; margin-right: auto;\">\n    ");
#nullable restore
#line 15 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/Store/Warehouse.cshtml"
Write(TempData["msg"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    <table class=\"table\">\n        <tr>\n            <th>\n                ");
#nullable restore
#line 19 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/Store/Warehouse.cshtml"
           Write(Html.DisplayNameFor(model => model.ProductID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 22 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/Store/Warehouse.cshtml"
           Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 25 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/Store/Warehouse.cshtml"
           Write(Html.DisplayNameFor(model => model.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th> \n            \n            <th>\n                ");
#nullable restore
#line 29 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/Store/Warehouse.cshtml"
           Write(Html.DisplayNameFor(model => model.BuyPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th> \n            \n            <th>\n                ");
#nullable restore
#line 33 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/Store/Warehouse.cshtml"
           Write(Html.DisplayNameFor(model => model.Stock));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th> \n        </tr>\n");
#nullable restore
#line 36 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/Store/Warehouse.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\n                <td>\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3e7400c45949269352094efd16fa6039e13d05d26425", async() => {
                WriteLiteral("\n                        ");
#nullable restore
#line 41 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/Store/Warehouse.cshtml"
                   Write(Html.DisplayFor(modelItem => item.ProductID));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 40 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/Store/Warehouse.cshtml"
                                                     WriteLiteral(item.ProductID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 45 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/Store/Warehouse.cshtml"
               Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    $");
#nullable restore
#line 48 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/Store/Warehouse.cshtml"
                Write(Html.DisplayFor(modelItem => item.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    $");
#nullable restore
#line 51 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/Store/Warehouse.cshtml"
                Write(Html.DisplayFor(modelItem => item.BuyPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 54 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/Store/Warehouse.cshtml"
               Write(Html.DisplayFor(modelItem => item.Stock));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                \n            </tr>\n");
#nullable restore
#line 58 "/Users/chuch/RiderProjects/ModuleEcheloneRebooted/ModuleEcheloneRebooted/Views/Store/Warehouse.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </table>\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ModuleEcheloneRebooted.Models.ProductModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
