#pragma checksum "C:\Users\ifpno\Documents\GitHub\igor\LFG\Projecto3MVC - Copia\Views\Login\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4920cadd064f36e41ac57189acd6fcb0e262aefd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Login_Index), @"mvc.1.0.view", @"/Views/Login/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\ifpno\Documents\GitHub\igor\LFG\Projecto3MVC - Copia\Views\_ViewImports.cshtml"
using Projecto3MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ifpno\Documents\GitHub\igor\LFG\Projecto3MVC - Copia\Views\_ViewImports.cshtml"
using Projecto3MVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4920cadd064f36e41ac57189acd6fcb0e262aefd", @"/Views/Login/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"37ffeabfcebb7e9cbec1c8deffc63e7144dc5074", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Login_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Projecto3MVC.Models.Ad_login>
    #nullable disable
    {
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\ifpno\Documents\GitHub\igor\LFG\Projecto3MVC - Copia\Views\Login\Index.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!DOCTYPE html>\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4920cadd064f36e41ac57189acd6fcb0e262aefd3564", async() => {
                WriteLiteral(@"
    <meta name=""viewport"" content=""width=device-width"" />
    <title>Login</title>
    <style>
        #login-div{
            position: absolute;
            left: 40%;
            top: 40%;
            border: 1px solid #ccc;
            padding: 10px 10px;
        }
        .field-validation-error{
            color:red;
        }
    </style>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4920cadd064f36e41ac57189acd6fcb0e262aefd4900", async() => {
                WriteLiteral("\r\n    <div id=\"login-div\">\r\n");
#nullable restore
#line 28 "C:\Users\ifpno\Documents\GitHub\igor\LFG\Projecto3MVC - Copia\Views\Login\Index.cshtml"
         using (Html.BeginForm(FormMethod.Post))
        {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"            <table>
                <tr>
                    <td></td>
                    <td style =""text-decoration:underline"">Northwind Database Manager</td>
                </tr>
                <tr>
                    <td>
                        ");
#nullable restore
#line 37 "C:\Users\ifpno\Documents\GitHub\igor\LFG\Projecto3MVC - Copia\Views\Login\Index.cshtml"
                   Write(Html.LabelFor(model => model.Admin_id));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 40 "C:\Users\ifpno\Documents\GitHub\igor\LFG\Projecto3MVC - Copia\Views\Login\Index.cshtml"
                   Write(Html.EditorFor(model => model.Admin_id));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <td></td>\r\n                    <td>");
#nullable restore
#line 45 "C:\Users\ifpno\Documents\GitHub\igor\LFG\Projecto3MVC - Copia\Views\Login\Index.cshtml"
                   Write(Html.ValidationMessageFor(model => model.Admin_id));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <td></td>\r\n                    <td>");
#nullable restore
#line 49 "C:\Users\ifpno\Documents\GitHub\igor\LFG\Projecto3MVC - Copia\Views\Login\Index.cshtml"
                   Write(Html.ValidationMessageFor(model => model.Ad_Password));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <td>");
#nullable restore
#line 52 "C:\Users\ifpno\Documents\GitHub\igor\LFG\Projecto3MVC - Copia\Views\Login\Index.cshtml"
                   Write(Html.LabelFor(model => model.Ad_Password));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 53 "C:\Users\ifpno\Documents\GitHub\igor\LFG\Projecto3MVC - Copia\Views\Login\Index.cshtml"
                   Write(Html.EditorFor(model => model.Ad_Password));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <td></td>\r\n                    <td>");
#nullable restore
#line 57 "C:\Users\ifpno\Documents\GitHub\igor\LFG\Projecto3MVC - Copia\Views\Login\Index.cshtml"
                   Write(Html.ValidationMessageFor(model => model.Ad_Password));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <td></td>\r\n                    <td>");
#nullable restore
#line 61 "C:\Users\ifpno\Documents\GitHub\igor\LFG\Projecto3MVC - Copia\Views\Login\Index.cshtml"
                   Write(Html.ValidationMessageFor(model => model.Ad_Password));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <td colspan=\"2\">\r\n                        <label class=\"field-validation-error\">");
#nullable restore
#line 65 "C:\Users\ifpno\Documents\GitHub\igor\LFG\Projecto3MVC - Copia\Views\Login\Index.cshtml"
                                                         Write(Html.DisplayFor(model => model.LoginErrorMessage));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <input type=""submit"" name=""name"" value=""Login""  />
                        <input type=""reset"" name=""name"" value=""Clear""  />
                    </td>
                </tr>
            </table>
");
#nullable restore
#line 76 "C:\Users\ifpno\Documents\GitHub\igor\LFG\Projecto3MVC - Copia\Views\Login\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"    </div>
    <script> src=""~/Scripts/jquery-1.10.2.min.js""></script>
    <script src=""https://code.jquery.com/jquery-1.10.2.min.js"" integrity=""sha256-C6CB9UYIS9UJeqinPHWTHVqh/E1uhG5Twh+Y5qFQmYg="" crossorigin=""anonymous""></script>
    <script src=""http://jqueryvalidation.org/files/dist/jquery.validate.js""></script>
    <script> src=""~/Scripts/jquery.validate.unobtrusive.min.js""></script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n\r\n</html>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Projecto3MVC.Models.Ad_login> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
