﻿using CCMS.ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace FleetOps.Helpers
{
    public static class CustomInputExtensions
    {

        public static MvcHtmlString CustomNgTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object lblHtmlAttributes = null, object txtHtmlAttributes = null, string additionalModel = null)
        {
            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression, lblRouteValueDictionary).ToHtmlString();

            var txtRouteValueDictionary = new RouteValueDictionary(txtHtmlAttributes);

            additionalModel = additionalModel == null ? null : additionalModel + ".";
            txtRouteValueDictionary.Add("ng-model", "_Object." + additionalModel + GetPropertyName(expression));
            if (txtRouteValueDictionary.ContainsKey("class"))
                txtRouteValueDictionary["class"] = txtRouteValueDictionary["class"] + " form-control";
            else
                txtRouteValueDictionary.Add("class", "form-control");
            txtRouteValueDictionary.Add("autocomplete", "on");

            if (NotAuthorized(htmlHelper, expression, txtRouteValueDictionary))
                return System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, expression, txtRouteValueDictionary);

            if (lblRouteValueDictionary.ContainsKey("required"))
            {
                Label = Label.Insert(Label.IndexOf("</label>"), "<span style='font-weight:bold;color:#B80A0A;margin-left:6px'>*</span>");
            }

            var ValidationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression);
            var validationspan = new TagBuilder("span");
            validationspan.AddCssClass("help-block");
            validationspan.InnerHtml = ValidationMessage.ToHtmlString();

            if (IsReadOnly(htmlHelper, expression, txtRouteValueDictionary))
                txtRouteValueDictionary["readonly"] = "readonly";


            var textBox = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, txtRouteValueDictionary);

            if (txtRouteValueDictionary.ContainsKey("emptyWrapper"))
            {
                return MvcHtmlString.Create(textBox.ToHtmlString() + validationspan.ToString());
            }
            else
            {
                return WrapinFormGroup(textBox.ToHtmlString() + validationspan.ToString(), Label);
            }
        }

        public static MvcHtmlString CustomNgDateRangeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression1, Expression<Func<TModel, TProperty>> expression2, string LabelText, object lblHtmlAttributes = null, object txtHtmlAttributes1 = null, object txtHtmlAttributes2 = null, string additionalModel = null)
        {
            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression1, LabelText, lblRouteValueDictionary).ToHtmlString();

            var txtRouteValueDictionary = new RouteValueDictionary(txtHtmlAttributes1);
            var txtRouteValueDictionary2 = new RouteValueDictionary(txtHtmlAttributes2);

            additionalModel = additionalModel == null ? null : additionalModel + ".";
            txtRouteValueDictionary.Add("ng-model", "_Object." +additionalModel+ GetPropertyName(expression1));
            txtRouteValueDictionary2.Add("ng-model", "_Object." +additionalModel+ GetPropertyName(expression2));
            txtRouteValueDictionary.Add("class", "form-control");
            txtRouteValueDictionary2.Add("class", "form-control");


            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("input-daterange input-group");
            wrapper.MergeAttribute("date-picker", "");

            var ValidationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression1);
            var ValidationMessage2 = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression2);
            var validationspan = new TagBuilder("span");
            validationspan.AddCssClass("help-block");
            validationspan.InnerHtml = ValidationMessage.ToHtmlString() + ValidationMessage2.ToHtmlString();

            if (lblRouteValueDictionary.ContainsKey("required"))
            {
                Label = Label.Insert(Label.IndexOf("</label>"), "<span style='font-weight:bold;color:#B80A0A;margin-left:6px'>*</span>");
            }

            if (NotAuthorized(htmlHelper, expression1, txtRouteValueDictionary))
            {
                wrapper.InnerHtml = System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, expression1, txtRouteValueDictionary).ToHtmlString();
            }
            else
            {
                if (IsReadOnly(htmlHelper, expression1, txtRouteValueDictionary))
                    txtRouteValueDictionary["readonly"] = "readonly";

                var textBox = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression1, txtRouteValueDictionary);
                wrapper.InnerHtml = textBox.ToHtmlString();
            }
            wrapper.InnerHtml += " <span class='input-group-addon'>to</span>";
            if (NotAuthorized(htmlHelper, expression2, txtRouteValueDictionary))
            {
                wrapper.InnerHtml += System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, expression2, txtRouteValueDictionary).ToHtmlString();
            }
            else
            {
                if (IsReadOnly(htmlHelper, expression2, txtRouteValueDictionary))
                    txtRouteValueDictionary2["readonly"] = "readonly";
                var textBox2 = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression2, txtRouteValueDictionary2);
                wrapper.InnerHtml += textBox2.ToHtmlString();
            }

            var final = WrapinFormGroup(wrapper.ToString() + validationspan.ToString(), Label);
            return final;
        }


      
        public static MvcHtmlString CustomNgTextRangeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression1, Expression<Func<TModel, TProperty>> expression2, string LabelText, object lblHtmlAttributes = null, object txtHtmlAttributes1 = null, object txtHtmlAttributes2 = null)
        {
            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression1, LabelText, lblRouteValueDictionary).ToHtmlString();

            var txtRouteValueDictionary = new RouteValueDictionary(txtHtmlAttributes1);
            var txtRouteValueDictionary2 = new RouteValueDictionary(txtHtmlAttributes2);
            txtRouteValueDictionary.Add("ng-model", "_Object." + GetPropertyName(expression1));
            txtRouteValueDictionary2.Add("ng-model", "_Object." + GetPropertyName(expression2));
            txtRouteValueDictionary.Add("class", "form-control");
            txtRouteValueDictionary2.Add("class", "form-control");


            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("input-daterange input-group");
            wrapper.MergeAttribute("date-picker", "");

            var ValidationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression1);
            var ValidationMessage2 = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression2);
            var validationspan = new TagBuilder("span");
            validationspan.AddCssClass("help-block");
            validationspan.InnerHtml = ValidationMessage.ToHtmlString() + ValidationMessage2.ToHtmlString();

            if (lblRouteValueDictionary.ContainsKey("required"))
            {
                Label = Label.Insert(Label.IndexOf("</label>"), "<span style='font-weight:bold;color:#B80A0A;margin-left:6px'>*</span>");
            }

            if (NotAuthorized(htmlHelper, expression1, txtRouteValueDictionary))
            {
                wrapper.InnerHtml = System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, expression1, txtRouteValueDictionary).ToHtmlString();
            }
            else
            {
                if (IsReadOnly(htmlHelper, expression1, txtRouteValueDictionary))
                    txtRouteValueDictionary["readonly"] = "readonly";

                var textBox = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression1, txtRouteValueDictionary);
                wrapper.InnerHtml = textBox.ToHtmlString();
            }
            wrapper.InnerHtml += " <span class='input-group-addon'>to</span>";
            if (NotAuthorized(htmlHelper, expression2, txtRouteValueDictionary))
            {
                wrapper.InnerHtml += System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, expression2, txtRouteValueDictionary).ToHtmlString();
            }
            else
            {
                if (IsReadOnly(htmlHelper, expression2, txtRouteValueDictionary))
                    txtRouteValueDictionary2["readonly"] = "readonly";
                var textBox2 = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression2, txtRouteValueDictionary2);
                wrapper.InnerHtml += textBox2.ToHtmlString();
            }

            var final = WrapinFormGroup(wrapper.ToString() + validationspan.ToString(), Label);
            return final;
        }


        public static MvcHtmlString CustomNgTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object lblHtmlAttributes = null, object txtHtmlAttributes = null, string additionalModel = null)
        {
            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression, lblRouteValueDictionary).ToHtmlString();

            var txtRouteValueDictionary = new RouteValueDictionary(txtHtmlAttributes);

            if (NotAuthorized(htmlHelper, expression, txtRouteValueDictionary))
            {
                return System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, expression, txtRouteValueDictionary);
            }
            else
            {
                if (IsReadOnly(htmlHelper, expression, txtRouteValueDictionary))
                    txtRouteValueDictionary["readonly"] = "readonly";
            }

            additionalModel = additionalModel == null ? null : additionalModel + ".";
            txtRouteValueDictionary.Add("ng-model", "_Object." +additionalModel+ GetPropertyName(expression));
            txtRouteValueDictionary.Add("class", "form-control");
            var textBox = System.Web.Mvc.Html.TextAreaExtensions.TextAreaFor(htmlHelper, expression, txtRouteValueDictionary);

            var ValidationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression);
            var validationspan = new TagBuilder("span");
            validationspan.AddCssClass("help-block");
            validationspan.InnerHtml = ValidationMessage.ToHtmlString();

            if (lblRouteValueDictionary.ContainsKey("required"))
            {
                Label = Label.Insert(Label.IndexOf("</label>"), "<span style='font-weight:bold;color:#B80A0A;margin-left:6px'>*</span>");
            }


            if (txtRouteValueDictionary.ContainsKey("emptyWrapper"))
            {
                return MvcHtmlString.Create(textBox.ToHtmlString() + validationspan.ToString());
            }
            else
            {
                return WrapinFormGroup(textBox.ToHtmlString() + validationspan.ToString(), Label);
            }
        }

        public static MvcHtmlString CustomNgNativeSelectListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> List_Item, object lblHtmlAttributes = null, object SelectHtmlAttributes = null, string Context = null)
        {
            string HtmlString = null;
            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression, lblRouteValueDictionary).ToHtmlString();
            var selectRouteValueDictionary = new RouteValueDictionary(SelectHtmlAttributes);
            var expressionName = GetPropertyName(expression);
            if (NotAuthorized(htmlHelper, expression, selectRouteValueDictionary))
            {
                return System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, expression, selectRouteValueDictionary);
            }
            else
            {
                if (IsReadOnly(htmlHelper, expression, selectRouteValueDictionary))
                    selectRouteValueDictionary["readonly"] = "readonly";
            }




            selectRouteValueDictionary.Add("ng-model", "_Object." + expressionName);
            selectRouteValueDictionary.Add("class", "form-control");
            selectRouteValueDictionary.Add("ng-options", "item.Value as item.Text for item in _Selects." + selectRouteValueDictionary["Name"]);
            var select = new TagBuilder("select");
            var dictionary = selectRouteValueDictionary.Select(p => new { p.Key, p.Value }).ToDictionary(p => p.Key, p => p.Value);
            select.MergeAttributes(dictionary);

            var ValidationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression);
            var validationspan = new TagBuilder("span");
            validationspan.AddCssClass("help-block");
            validationspan.InnerHtml = "<span class='field-validation-valid' data-valmsg-for='" + selectRouteValueDictionary["Name"].ToString() + "' data-valmsg-replace='true'></span>";
            if (IsReadOnly(htmlHelper, expression, selectRouteValueDictionary))
            {
                select.Attributes.Add("disabled", "disabled");
                HtmlString = select.ToString() + validationspan.ToString();
                HtmlString += System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, expression, selectRouteValueDictionary).ToHtmlString();
            }
            else
            {
                HtmlString = select.ToString() + validationspan.ToString();
            }
            if (selectRouteValueDictionary.ContainsKey("emptyWrapper"))
            {
                return MvcHtmlString.Create(HtmlString);
            }
            else
            {
                return WrapinFormGroup(HtmlString, Label);
            }
        }

        public static MvcHtmlString CustomNgSelectListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> List_Item, object lblHtmlAttributes = null, object SelectHtmlAttributes = null, object selectChoiceOptions = null, string Context = null, string additionalModel = null)
        {
            //<h3>Select2 theme</h3>
            //<p>Selected: {{person.selected}}</p>
            //<ui-select ng-model="person.selected" theme="select2" ng-disabled="disabled" style="min-width: 300px;">
            //  <ui-select-match placeholder="Select a person in the list or search his name/age...">{{$select.selected.name}}</ui-select-match>
            //  <ui-select-choices repeat="person in people | propsFilter: {name: $select.search, age: $select.search}">
            //    <div ng-bind-html="person.name | highlight: $select.search"></div>
            //    <small>
            //      email: {{person.email}}
            //      age: <span ng-bind-html="''+person.age | highlight: $select.search"></span>
            //    </small>
            //  </ui-select-choices>
            //</ui-select>

                var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
                var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression, lblRouteValueDictionary).ToHtmlString();

                var selectRouteValueDictionary = new RouteValueDictionary(SelectHtmlAttributes);
                var choicesRouteValueDictionary = new RouteValueDictionary(selectChoiceOptions);

                if (NotAuthorized(htmlHelper, expression, selectRouteValueDictionary))
                {
                    return System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, expression, selectRouteValueDictionary);
                }


                var expressionName = GetPropertyName(expression);
                var ValidationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression);
                var validationspan = new TagBuilder("span");
                validationspan.AddCssClass("help-block");
                validationspan.InnerHtml = "<span class='field-validation-valid' data-valmsg-for='" + selectRouteValueDictionary["Name"].ToString() + "' data-valmsg-replace='true'></span>";
                var dictionary = selectRouteValueDictionary.Select(p => new { p.Key, p.Value }).ToDictionary(p => p.Key, p => p.Value);


                if (IsReadOnly(htmlHelper, expression, selectRouteValueDictionary))
                {
                    dictionary.Add("ng-disabled", "true");
                }
                var uiSelect = new TagBuilder("ui-select");
                additionalModel = additionalModel == null ? null : additionalModel + ".";
                uiSelect.MergeAttribute("ng-model", "_Object." + additionalModel + expressionName);
                uiSelect.MergeAttribute("theme", "select2");
                uiSelect.MergeAttribute("style", "width:100%");
                uiSelect.MergeAttributes(dictionary);
                var uiSelectmatch = new TagBuilder("ui-select-match");
                uiSelectmatch.InnerHtml = "{{$select.selected.Text}}";

                var uiSelectChoices = new TagBuilder("ui-select-choices");
                uiSelectChoices.MergeAttributes(choicesRouteValueDictionary);
                uiSelectChoices.MergeAttribute("value", "{{$select.selected.Value}}");
                uiSelectChoices.MergeAttribute("repeat", "item.Value as item in _Selects." + selectRouteValueDictionary["Name"] + "| filter: $select.search");
                var uiBindHtml = new TagBuilder("div");
                uiBindHtml.MergeAttribute("ng-bind-html", "item.Text | highlight: $select.search");


                uiSelect.InnerHtml = uiSelectmatch.ToString();
                uiSelectChoices.InnerHtml = uiBindHtml.ToString();
                uiSelect.InnerHtml += uiSelectChoices.ToString();

                if (lblRouteValueDictionary.ContainsKey("required"))
                {
                    Label = Label.Insert(Label.IndexOf("</label>"), "<span style='font-weight:bold;color:#B80A0A;margin-left:6px'>*</span>");
                }

                if (selectRouteValueDictionary.ContainsKey("emptyWrapper"))
                {
                    return MvcHtmlString.Create(uiSelect.ToString() + validationspan.ToString());
                }
                else
                {
                    return WrapinFormGroup(uiSelect.ToString() + validationspan.ToString(), Label);
                }
 
        }

        public static MvcHtmlString CustomNgMultiSelectListFor2<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> List_Item, object lblHtmlAttributes = null, object SelectHtmlAttributes = null, string Context = null)
        {
            //<h3>Select2 theme</h3>
            //<p>Selected: {{person.selected}}</p>
            //<ui-select ng-model="person.selected" theme="select2" ng-disabled="disabled" style="min-width: 300px;">
            //  <ui-select-match placeholder="Select a person in the list or search his name/age...">{{$select.selected.name}}</ui-select-match>
            //  <ui-select-choices repeat="person in people | propsFilter: {name: $select.search, age: $select.search}">
            //    <div ng-bind-html="person.name | highlight: $select.search"></div>
            //    <small>
            //      email: {{person.email}}
            //      age: <span ng-bind-html="''+person.age | highlight: $select.search"></span>
            //    </small>
            //  </ui-select-choices>
            //</ui-select> 

            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression, lblRouteValueDictionary).ToHtmlString();

            var selectRouteValueDictionary = new RouteValueDictionary(SelectHtmlAttributes);

            if (NotAuthorized(htmlHelper, expression, selectRouteValueDictionary))
            {
                return System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, expression, selectRouteValueDictionary);
            }


            var expressionName = GetPropertyName(expression);
            var ValidationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression);
            var validationspan = new TagBuilder("span");
            validationspan.AddCssClass("help-block");
            validationspan.InnerHtml = "<span class='field-validation-valid' data-valmsg-for='" + selectRouteValueDictionary["Name"].ToString() + "' data-valmsg-replace='true'></span>";
            var dictionary = selectRouteValueDictionary.Select(p => new { p.Key, p.Value }).ToDictionary(p => p.Key, p => p.Value);


            if (IsReadOnly(htmlHelper, expression, selectRouteValueDictionary))
            {
                dictionary.Add("ng-disabled", "true");
            }

            var uiSelect = new TagBuilder("ui-select");
            uiSelect.MergeAttribute("multiple", "");
            uiSelect.MergeAttribute("ng-model", "_Object." + expressionName);
            uiSelect.MergeAttribute("theme", "select2");
            uiSelect.MergeAttribute("style", "width:100%");
            uiSelect.MergeAttributes(dictionary);
            var uiSelectmatch = new TagBuilder("ui-select-match");
            uiSelectmatch.InnerHtml = "{{$item.Text}}";


            var uiSelectChoices = new TagBuilder("ui-select-choices");
            uiSelectChoices.MergeAttribute("value", "{{$select.selected.Value}}");
            uiSelectChoices.MergeAttribute("repeat", "item.Value as item in _Selects." + selectRouteValueDictionary["Name"] + " | filter: $select.search");
            var uiBindHtml = new TagBuilder("div");
            uiBindHtml.MergeAttribute("ng-bind-html", "item.Text | highlight: $select.search");

            uiSelect.InnerHtml = uiSelectmatch.ToString();
            uiSelectChoices.InnerHtml = uiBindHtml.ToString();
            uiSelect.InnerHtml += uiSelectChoices.ToString();

            if (lblRouteValueDictionary.ContainsKey("required"))
            {
                Label = Label.Insert(Label.IndexOf("</label>"), "<span style='font-weight:bold;font-size:15px;color:#B80A0A;margin-left:6px'>*</span>");
            }

            if (selectRouteValueDictionary.ContainsKey("emptyWrapper"))
            {
                return MvcHtmlString.Create(uiSelect.ToString() + validationspan.ToString());
            }
            else
            {
                return WrapinFormGroup(uiSelect.ToString() + validationspan.ToString(), Label);
            }
        }

        public static MvcHtmlString CustomNgMultiSelectListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> List_Item, object lblHtmlAttributes = null, object SelectHtmlAttributes = null, string Context = null)
        {
            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression, lblRouteValueDictionary).ToHtmlString();

            var selectRouteValueDictionary = new RouteValueDictionary(SelectHtmlAttributes);
            var expressionName = GetPropertyName(expression);
            selectRouteValueDictionary.Add("ng-model", "_Object." + expressionName);
            selectRouteValueDictionary.Add("placeholder", "select");
            selectRouteValueDictionary.Add("class", "form-control");
            selectRouteValueDictionary.Add("ng-multiple", "true");
            selectRouteValueDictionary.Add("ng-options", "item.Value as item.Text for item in _Selects." + selectRouteValueDictionary["Name"]);
            var select = new TagBuilder("select"); select.Attributes.Add("multiple", "");
            var dictionary = selectRouteValueDictionary.Select(p => new { p.Key, p.Value }).ToDictionary(p => p.Key, p => p.Value);
            select.MergeAttributes(dictionary);

            var ValidationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression);
            var validationspan = new TagBuilder("span");
            validationspan.AddCssClass("help-block");
            validationspan.InnerHtml = "<span class='field-validation-valid' data-valmsg-for='" + selectRouteValueDictionary["Name"].ToString() + "' data-valmsg-replace='true'></span>";


            if (selectRouteValueDictionary.ContainsKey("emptyWrapper"))
            {
                return MvcHtmlString.Create(select.ToString() + validationspan.ToString());
            }
            else
            {
                return WrapinFormGroup(select.ToString() + validationspan.ToString(), Label);
            }
        }


        public static MvcHtmlString WrapinFormGroup(string html, string Label, bool LabelIsHtml = true)
        {
            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            if (!LabelIsHtml)
            {
                var label = new TagBuilder("label");
                label.SetInnerText(Label);
                div.InnerHtml = label.ToString(TagRenderMode.SelfClosing);
            }
            else
            {
                div.InnerHtml = Label;
            }
            div.InnerHtml += html;
            return MvcHtmlString.Create(div.ToString());
        }

        public static MvcHtmlString CustomNgRadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> Expression, IEnumerable<SelectListItem> List_Item, object lblHtmlAttributes = null, object RadioHtmlAttributes = null, string Context = null)
        {
            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            var _Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, Expression, lblRouteValueDictionary).ToHtmlString();


            var RadioRouteValueDictionary = new RouteValueDictionary(RadioHtmlAttributes);
            var expressionName = GetPropertyName(Expression);


            var Label = new TagBuilder("label");
            Label.AddCssClass("radio-inline");
            Label.Attributes.Add("ng-repeat", "item in _Selects." + RadioRouteValueDictionary["name"]);

            if (NotAuthorized(htmlHelper, Expression, RadioRouteValueDictionary))
                return System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, Expression, RadioRouteValueDictionary);

            RadioRouteValueDictionary.Add("ng-model", "_Object." + expressionName);
            RadioRouteValueDictionary.Add("type", "radio");
            RadioRouteValueDictionary["name"] = expressionName;
            RadioRouteValueDictionary.Add("checked", "checked");
            RadioRouteValueDictionary.Add("value", "{{item.Value}}");
            var Radio = new TagBuilder("input");
            var dictionary = RadioRouteValueDictionary.Select(p => new { p.Key, p.Value }).ToDictionary(p => p.Key, p => p.Value);
            Radio.MergeAttributes(dictionary);
            Label.InnerHtml = Radio.ToString();
            Label.InnerHtml += "{{item.Text}}";
            //<label class="radio-inline" for="inline-radio1">
            //                                <input type="radio" id="inline-radio1" name="inline-radios" value="option1">One
            //                            </label>


            if (RadioRouteValueDictionary.ContainsKey("emptyWrapper"))
            {
                return MvcHtmlString.Create("<br/>" + Label.ToString());
            }
            else
            {
                return WrapinFormGroup("<br/>" + Label.ToString(), _Label);
            }

        }



        public static MvcHtmlString CustomNgCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, object lblHtmlAttributes = null, object checkHtmlAttributes = null, string Context = null)//, object txtHtmlAttributes = null
        {
            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            var CheckBoxRouteValueDictionary = new RouteValueDictionary(checkHtmlAttributes);
            //var txtRouteValueDictionary = new RouteValueDictionary(txtHtmlAttributes);
            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression, lblRouteValueDictionary).ToHtmlString();
            var expressionName = GetPropertyName(expression);

            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var displayName = metadata.DisplayName;

            var controlGroupDiv = new TagBuilder("div");
            controlGroupDiv.AddCssClass("checkbox");

            var label = new TagBuilder("label");


            var checkBox = new TagBuilder("input");
            checkBox.Attributes.Add("type", "checkbox");
            checkBox.MergeAttributes(CheckBoxRouteValueDictionary);
            checkBox.Attributes.Add("ng-model", "_Object." + expressionName);
            if (CheckBoxRouteValueDictionary.ContainsKey("ng_disabled"))
            {
                checkBox.Attributes.Add("ng-disabled", CheckBoxRouteValueDictionary["ng_disabled"].ToString());
            }

            if (CheckBoxRouteValueDictionary.ContainsKey("ng_click"))
            {
                checkBox.Attributes.Add("ng-click", CheckBoxRouteValueDictionary["ng_click"].ToString());
            }

            if (IsReadOnly(htmlHelper, expression, CheckBoxRouteValueDictionary))
            {
                checkBox.Attributes.Add("disabled", "disabled");
            }

            var validationspan = new TagBuilder("span");
            var textBox = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, CheckBoxRouteValueDictionary);

            if (NotAuthorized(htmlHelper, expression, CheckBoxRouteValueDictionary))
                return System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, expression, CheckBoxRouteValueDictionary);

            //if (CheckBoxRouteValueDictionary.ContainsKey("emptyWrapper"))
            //{
            //    return MvcHtmlString.Create(textBox.ToHtmlString() + validationspan.ToString());
            //}
            //else
            //{
            //    return WrapinFormGroup(textBox.ToHtmlString() + validationspan.ToString(), Label);
            //}

            label.InnerHtml = checkBox.ToString(TagRenderMode.SelfClosing);
            label.InnerHtml += htmlHelper.Encode(displayName);

            controlGroupDiv.InnerHtml = label.ToString();

            return MvcHtmlString.Create(controlGroupDiv.ToString());

        }
        public static MvcHtmlString CustomNgHiddenCtrlStatusFor<TModel>(this HtmlHelper<TModel> helper, string SectionCd, string SectionName, string CtrlName, object hiddenAttrs = null)
        {
            var HiddenRouteValueDictionary = new RouteValueDictionary(hiddenAttrs);
            HiddenRouteValueDictionary.Add("ng-model", !string.IsNullOrEmpty(SectionCd) ? SectionCd : SectionName + "_" + CtrlName);
            var Value = GetControlStatus(helper, CtrlName, SectionCd, SectionName);
            var HiddenField = System.Web.Mvc.Html.InputExtensions.Hidden(helper, CtrlName, Value, HiddenRouteValueDictionary);
            return MvcHtmlString.Create(HiddenField.ToHtmlString());
        }
        public static string GetPropertyName<TModel, TProperty>(System.Linq.Expressions.Expression<Func<TModel, TProperty>> property)
        {
            System.Linq.Expressions.LambdaExpression lambda = (System.Linq.Expressions.LambdaExpression)property;
            System.Linq.Expressions.MemberExpression memberExpression;

            if (lambda.Body is System.Linq.Expressions.UnaryExpression)
            {
                System.Linq.Expressions.UnaryExpression unaryExpression = (System.Linq.Expressions.UnaryExpression)(lambda.Body);
                memberExpression = (System.Linq.Expressions.MemberExpression)(unaryExpression.Operand);
            }
            else
            {
                memberExpression = (System.Linq.Expressions.MemberExpression)(lambda.Body);
            }
            var xx = (PropertyInfo)memberExpression.Member;
            return ((PropertyInfo)memberExpression.Member).Name;
        }

        static string Check<T>(Expression<Func<T>> expr)
        {
            var body = ((MemberExpression)expr.Body);
            Console.WriteLine("Name is: {0}", body.Member.Name);
            Console.WriteLine("Value is: {0}", ((FieldInfo)body.Member)
           .GetValue(((ConstantExpression)body.Expression).Value));
            return ((PropertyInfo)body.Member).Name;
        }


        //
        //Deprecated
        //
        public static MvcHtmlString CustomSelectListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> SelectList, object lblHtmlAttributes = null, object SelectHtmlAttributes = null, string Context = null)
        {
            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            var selectRouteValueDictionary = new RouteValueDictionary(SelectHtmlAttributes);

            var controlGroupDiv = new TagBuilder("div");
            controlGroupDiv.AddCssClass(selectRouteValueDictionary.ContainsKey("required") ? "form-group has-feedback" : "form-group");

            lblRouteValueDictionary["class"] = "control-label col-sm-3";



            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression, lblRouteValueDictionary).ToHtmlString();
            if (SelectList == null)
                SelectList = new List<SelectListItem>();
            var selectList = System.Web.Mvc.Html.SelectExtensions.DropDownListFor(htmlHelper, expression, SelectList);

            var selectWrapper = new TagBuilder("div");
            selectWrapper.AddCssClass("col-sm-9");
            selectWrapper.InnerHtml = selectList.ToHtmlString();



            if (selectRouteValueDictionary.ContainsKey("required"))
            {
                selectWrapper.InnerHtml += "<span class='fa fa-asterisk form-control-feedback'></span>";
            }

            var ValidationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression);
            var validationspan = new TagBuilder("span");
            validationspan.AddCssClass("help-inline");
            validationspan.InnerHtml = ValidationMessage.ToHtmlString();
            selectWrapper.InnerHtml += validationspan.InnerHtml;
            controlGroupDiv.InnerHtml = Label;
            controlGroupDiv.InnerHtml += selectWrapper.ToString(TagRenderMode.EndTag);
            return MvcHtmlString.Create(controlGroupDiv.ToString(TagRenderMode.EndTag));
        }

        public static MvcHtmlString CustomRadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> SelectList, object lblHtmlAttributes = null, object radioHtmlAttributes = null, string Context = null)
        {
            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            var RadioButtonRouteValueDictionary = new RouteValueDictionary(radioHtmlAttributes);
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var controlGroupDiv = new TagBuilder("div");
            controlGroupDiv.AddCssClass("form-group");
            lblRouteValueDictionary["class"] = "control-label col-sm-3";
            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression, lblRouteValueDictionary).ToHtmlString();
            var builder = new System.Text.StringBuilder();
            if (SelectList != null)
            {
                foreach (SelectListItem item in SelectList)
                {
                    var id = string.Format("{0}_{1}", metaData.PropertyName, item.Value);
                    builder.Append("<label class='radio'>");
                    var radio2 = System.Web.Mvc.Html.InputExtensions.RadioButtonFor(htmlHelper, expression, item.Value, RadioButtonRouteValueDictionary);
                    builder.Append(radio2.ToHtmlString());
                    builder.Append(item.Text + "</label>");
                }
            }



            var controlWrapper = new TagBuilder("div");
            controlWrapper.AddCssClass("col-sm-9");
            controlWrapper.InnerHtml = builder.ToString();
            controlGroupDiv.InnerHtml = Label;
            controlGroupDiv.InnerHtml += controlWrapper.ToString();
            return MvcHtmlString.Create(controlGroupDiv.ToString());
        }

        public static MvcHtmlString CustomTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object lblHtmlAttributes = null, object txtHtmlAttributes = null, string Context = null)
        {
            var txtRouteValueDictionary = new RouteValueDictionary(txtHtmlAttributes);
            var controlGroupDiv = new TagBuilder("div");
            controlGroupDiv.AddCssClass(txtRouteValueDictionary.ContainsKey("required") ? "form-group has-feedback" : "form-group");

            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            lblRouteValueDictionary["class"] = "control-label col-sm-3";



            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression, lblRouteValueDictionary).ToHtmlString();
            var textBox = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, txtRouteValueDictionary);

            var textWrapper = new TagBuilder("div");
            textWrapper.AddCssClass("col-sm-9");

            textWrapper.InnerHtml = textBox.ToHtmlString();

            if (txtRouteValueDictionary.ContainsKey("required"))
            {
                textWrapper.InnerHtml += "<span class='fa fa-asterisk form-control-feedback'></span>";
            }

            var ValidationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression);
            var validationspan = new TagBuilder("span");
            validationspan.AddCssClass("help-inline");
            validationspan.InnerHtml = ValidationMessage.ToHtmlString();

            textWrapper.InnerHtml += validationspan.InnerHtml;

            controlGroupDiv.InnerHtml = Label;
            controlGroupDiv.InnerHtml += textWrapper.ToString();
            return MvcHtmlString.Create(controlGroupDiv.ToString());
        }

        public static bool NotAuthorized<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary dir = null)
        {
            string SectionName = null;
            Accessibility isAccessible;
            if (htmlHelper.ViewData["Accessibility"] == null)
                return false;
            var accessibility = (List<Accessibility>)htmlHelper.ViewData["Accessibility"];
            // var ControlId = context + "." + ExpressionHelper.GetExpressionText(expression);
          //  var ControlId = ExpressionHelper.GetExpressionText(expression).ToLower().Split('.').LastOrDefault();
            var ControlId = !string.IsNullOrEmpty(Convert.ToString(dir["controlId"])) ? dir["controlId"].ToString().ToLower() : ExpressionHelper.GetExpressionText(expression).ToLower().Split('.').LastOrDefault();

            if (dir != null && dir.ContainsKey("section"))
            {
                SectionName = string.IsNullOrEmpty(dir["section"].ToString()) ? "NULL" : dir["section"].ToString();
                isAccessible = accessibility.FirstOrDefault(p => p.CtrlId.ToLower().Equals(ControlId) && p.SectionShortCd.ToLower().Equals(SectionName.ToLower()));
            }
            else if (dir != null && dir.ContainsKey("sectionName"))
            {
                isAccessible = accessibility.FirstOrDefault(p => p.CtrlId.ToLower().Equals(ControlId) && p.SectionName.ToLower().Equals(dir["sectionName"].ToString().ToLower()));
            }
            else
            {
                isAccessible = accessibility.FirstOrDefault(p => p.CtrlId.ToLower().Equals(ControlId));
            }
            if (isAccessible == null)
                return false;
            return isAccessible.Sts == 0 ? true : false;
        }
        public static bool NotAuthorized<TModel>(this HtmlHelper<TModel> helper, string sectionCode, string CtrlName, string SectionName = null)
        {
            Accessibility isAccessible = null;
            if (string.IsNullOrEmpty(sectionCode) && string.IsNullOrEmpty(SectionName))
            {
                sectionCode = "NULL";
            }
            if (helper.ViewData["Accessibility"] == null)
                return false;
            var accessibility = (List<Accessibility>)helper.ViewData["Accessibility"];//Accessibility
            if (accessibility == null)
                return false;

            if (!string.IsNullOrEmpty(sectionCode))
            {
                isAccessible = accessibility.FirstOrDefault(p => p.CtrlId.ToLower().Equals(CtrlName.ToLower()) && p.SectionShortCd.ToLower().Equals(sectionCode.ToLower()));
            }
            else
            {
                isAccessible = accessibility.FirstOrDefault(p => p.CtrlId.ToLower().Equals(CtrlName.ToLower()) && p.SectionName.ToLower().Equals(SectionName.ToLower()));
            }
            if (isAccessible == null)
                return false;
            return isAccessible.Sts == 0 ? true : false;
        }


        public static string GetControlStatus<TModel>(this HtmlHelper<TModel> helper, string CtrlName, string SectionCd, string SectionName = null)
        {
            if (helper.ViewData["Accessibility"] == null)
                return "1";
            Accessibility status = null;
            var accessibility = (List<Accessibility>)helper.ViewData["Accessibility"];
            if (!string.IsNullOrEmpty(SectionCd))
                status = accessibility.FirstOrDefault(p => p.CtrlId.ToLower().Equals(CtrlName.ToLower()) && p.SectionShortCd.ToLower().Equals(SectionCd.ToLower()));
            else
                status = accessibility.FirstOrDefault(p => p.CtrlId.ToLower().Equals(CtrlName.ToLower()) && p.SectionName.ToLower().Equals(SectionName.ToLower()));
            if (status == null)
                return "1";
            return status.Sts.ToString();
        }

        public static HtmlString CheckRestricted<TModel>(this HtmlHelper<TModel> helper, string CtrlName, string SectionName, string output = null)
        {
            var x = GetControlStatus(helper, CtrlName, SectionName);
            if (!string.IsNullOrEmpty(output))
            {
                return (x == "1" ? MvcHtmlString.Create(output) : MvcHtmlString.Create(""));
            }
            return MvcHtmlString.Create("");
        }

        public static bool IsReadOnly<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary dir)
        {
            string SectionName = null;
            Accessibility isReadOnly;

            if (HttpContext.Current.Session["CtrlAccessibility"] == null)
                return false;
            var accessibility = (List<Accessibility>)HttpContext.Current.Session["CtrlAccessibility"];
            // var ControlId = context + "." + ExpressionHelper.GetExpressionText(expression);
            var ControlId = !string.IsNullOrEmpty( Convert.ToString( dir["controlId"])) ? dir["controlId"].ToString().ToLower(): ExpressionHelper.GetExpressionText(expression).ToLower().Split('.').LastOrDefault();

            if (dir != null && dir.ContainsKey("section"))
            {
                SectionName = string.IsNullOrEmpty(dir["section"].ToString()) ? "NULL" : dir["section"].ToString();
                isReadOnly = accessibility.FirstOrDefault(p => p.CtrlId.ToLower().Equals(ControlId) && p.SectionShortCd.ToLower().Equals(SectionName.ToLower()));
            }
            else if (dir != null && dir.ContainsKey("sectionName"))
            {
                isReadOnly = accessibility.FirstOrDefault(p => p.CtrlId.ToLower().Equals(ControlId) && p.SectionName.ToLower().Equals(dir["sectionName"].ToString().ToLower()));
            }
            else
            {
                isReadOnly = accessibility.FirstOrDefault(p => p.CtrlId.ToLower().Equals(ControlId));
            }

            if (isReadOnly == null)
                return false;
            return isReadOnly.Sts == 2 ? true : false;
        }


        public static bool IsReadOnly<TModel>(this HtmlHelper<TModel> helper, string sectionCode, string CtrlName, string SectionName = null)
        {
            Accessibility isAccessible;
            if (helper.ViewData["Accessibility"] == null)
                return false;
            if (string.IsNullOrEmpty(sectionCode) && string.IsNullOrEmpty(SectionName))
                sectionCode = "NULL";
            var accessibility = (List<Accessibility>)helper.ViewData["Accessibility"];

            if (!string.IsNullOrEmpty(sectionCode))
            {
                isAccessible = accessibility.FirstOrDefault(p => p.CtrlId.ToLower().Equals(CtrlName.ToLower()) && p.SectionShortCd.ToLower().Equals(sectionCode.ToLower()));
            }
            else
            {
                isAccessible = accessibility.FirstOrDefault(p => p.CtrlId.ToLower().Equals(CtrlName.ToLower()) && p.SectionName.ToLower().Equals(SectionName.ToLower()));
            }
            if (isAccessible == null)
                return false;
            return isAccessible.Sts == 2 ? true : false;



        }

        public static bool SectionIsEnabled<TModel>(this HtmlHelper<TModel> helper, string SectionShortCd, string SectionName = null)
        {
            Accessibility Section = null;
            if (helper.ViewData["Accessibility"] == null)
                return false;
            var accessibility = (List<Accessibility>)helper.ViewData["Accessibility"];
            if (!string.IsNullOrEmpty(SectionShortCd))
                Section = accessibility.FirstOrDefault(p => p.SectionShortCd.ToLower() == SectionShortCd.ToLower());
            else
                Section = accessibility.FirstOrDefault(p => p.SectionName.ToLower() == SectionName.ToLower());
            if (Section == null)
                return false;
            return Section.Sts == 0 ? false : true;
        }
    }
}