#pragma checksum "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\Pages\PersonsPage.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "898f8c5db0396b27ff319b716ecdffe722e65920"
// <auto-generated/>
#pragma warning disable 1591
namespace Demo.ui.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\_Imports.razor"
using Demo.ui;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\_Imports.razor"
using Demo.ui.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\Pages\PersonsPage.razor"
using Demo.Api.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\Pages\PersonsPage.razor"
using Demo.Api.Controllers;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/person")]
    public partial class PersonsPage : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>People Page</h1>\r\n\r\n");
            __builder.AddMarkupContent(1, "<h4>Insert New Person</h4>\r\n");
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Forms.EditForm>(2);
            __builder.AddAttribute(3, "Model", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Object>(
#nullable restore
#line 7 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\Pages\PersonsPage.razor"
                  newPerson

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(4, "OnValidSubmit", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Forms.EditContext>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Forms.EditContext>(this, 
#nullable restore
#line 7 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\Pages\PersonsPage.razor"
                                             InsertPerson

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(5, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Forms.EditContext>)((context) => (__builder2) => {
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.DataAnnotationsValidator>(6);
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(7, "\r\n    ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.ValidationSummary>(8);
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(9, "\r\n\r\n    ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(10);
                __builder2.AddAttribute(11, "id", "firstName");
                __builder2.AddAttribute(12, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 11 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\Pages\PersonsPage.razor"
                                           newPerson.firstName

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(13, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => newPerson.firstName = __value, newPerson.firstName))));
                __builder2.AddAttribute(14, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => newPerson.firstName));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(15, "\r\n    ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(16);
                __builder2.AddAttribute(17, "id", "lastName");
                __builder2.AddAttribute(18, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 12 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\Pages\PersonsPage.razor"
                                          newPerson.lastName

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(19, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => newPerson.lastName = __value, newPerson.lastName))));
                __builder2.AddAttribute(20, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => newPerson.lastName));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(21, "\r\n    ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(22);
                __builder2.AddAttribute(23, "id", "emailAddress");
                __builder2.AddAttribute(24, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 13 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\Pages\PersonsPage.razor"
                                              newPerson.email

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(25, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => newPerson.email = __value, newPerson.email))));
                __builder2.AddAttribute(26, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => newPerson.email));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(27, "\r\n\r\n    ");
                __builder2.AddMarkupContent(28, "<button type=\"submit\" class=\"btn btn-primary\">Submit</button>");
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 18 "E:\WadeProject\Wet-Project-Web\NewMethod\Demo.ui\Demo.ui\Pages\PersonsPage.razor"
       
    private Person newPerson = new Person();

    private async Task InsertPerson()
    {
        ProductController person = new ProductController();
        person.insertPeople(newPerson);
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591