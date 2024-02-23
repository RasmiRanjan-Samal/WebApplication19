@model WebApplication19.ViewModels.LoginVM

@{
    ViewBag.Title = "Login";
    Layout = "~/Views/Shared/_Layout1.cshtml";
}

<h2>Login</h2>


@using (Html.BeginForm()) 
{
    @Html.AntiForgeryToken()
    
    <div class="form-horizontal">
      
        <hr />
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
        <div class="form-group">
            @Html.LabelFor(model => model.Username, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Username, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.Username, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.Password, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Password, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.Password, "", new { @class = "text-danger" })
            </div>
        </div>
         

        <div class="form-group">
            <div class="col-md-offset-2  col-md-10">
                <input type="submit" value="Login" class="btn btn-primary" />
            </div>
        </div>
    </div>
    @ViewBag.Error
}

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

@section Scripts {
    @Scripts.Render("~/bundles/jqueryval")
}
