using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using find.Areas.Common.Helpers;
using System.Configuration;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace find.Areas.Common.Helpers
{
    public static class HtmlHelpers
    {
        public static HtmlString HtmlSortHelpers(this HtmlHelper htmlHelper, WebGrid grid)
        {
            var str = new StringBuilder();
            str.AppendLine("<script type='text/javascript'>");
            str.AppendLine("var html = '<div style=\"float:right\" class=\"link-sort\"><div style=\"height:5px\"><i class=\"fa fa-sort-up\" style=\"margin-top:2px\"></i></div><div style=\"height:5px\"><i class=\"fa fa-sort-desc\" style=\"margin-top:-2px\"></i></div></div>';");
            str.AppendLine("$('thead > tr > th > a').parent().append(html);");
            if (!string.IsNullOrWhiteSpace(grid.SortColumn))
            {
                str.AppendLine("var htmlActive = '<div style=\"float:right\" class=\"link-sort\"><div style=\"height:5px\"><i class=\"fa fa-sort-up " + (grid.SortDirection == SortDirection.Ascending ? "sort-active" : "") + "\" style=\"margin-top:2px\"></i></div><div style=\"height:5px\"><i class=\"fa fa-sort-desc " + (grid.SortDirection != SortDirection.Ascending ? "sort-active" : "") + "\" style=\"margin-top:-2px\"></i></div></div>';");
                str.AppendLine("$('thead > tr > th > a[href*=\"sort=" + grid.SortColumn + "\"]').parent().find('div.link-sort').remove();");
                str.AppendLine("$('thead > tr > th > a[href*=\"sort=" + grid.SortColumn + "\"]').parent().append(htmlActive);");
            }
            str.AppendLine("</script>");
            return new HtmlString(str.ToString());
        }

        public static HtmlString HtmlDateHelpers(this HtmlHelper htmlHelper, DateTime dt, string url, string idContent)
        {
            var str = new StringBuilder();
            if (!string.IsNullOrEmpty(dt.ToString()))
            {
                string ym = dt.ToString("yyyy/MM");
                Calendar Calendar = new Calendar();
            }
            return new HtmlString(str.ToString());
        }

        public static MvcHtmlString ComboBox(this HtmlHelper html, string name, SelectList items, string selectedValue)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(html.DropDownList(name + "_hidden", items, new { @style = "width: 200px;", @onchange = "$('input#" + name + "').val($(this).val());" }));
            sb.Append(html.TextBox(name, selectedValue, new { @style = "margin-left: -199px; width: 179px; height: 1.2em; border: 0;" }));
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString ComboBoxFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, SelectList items)
        {
            MemberExpression me = (MemberExpression)expression.Body;
            string name = me.Member.Name;

            StringBuilder sb = new StringBuilder();
            sb.Append(html.DropDownList(name + "_hidden", items, new { @style = "width: 200px;", @onchange = "$('input#" + name + "').val($(this).val());" }));
            sb.Append(html.TextBoxFor(expression, new { @style = "margin-left: -199px; width: 179px; height: 1.2em; border: 0;" }));
            return MvcHtmlString.Create(sb.ToString());
        }

        public static IHtmlString CustomListBox(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes, bool reqFlg)
        {
            string @class = string.Empty;
            string @style = string.Empty;
            string id = string.Empty;
            string disabled = string.Empty;
            int size = 0;

            CustomListBox customListBox = new CustomListBox();
            customListBox.GetAttributes(ref htmlAttributes, ref @class, ref @style, ref size, ref id, ref disabled, reqFlg);

            MvcHtmlString listBox = SelectExtensions.ListBox(htmlHelper, name, selectList, htmlAttributes);
            string strHtml = string.Empty;

            strHtml = customListBox.ListBoxForMobile(@class, @style, size, id, selectList, disabled, reqFlg);

            return htmlHelper.Raw(listBox.ToHtmlString() + strHtml);
        }

        public static IHtmlString CustomListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes, bool reqFlg)
        {
            string @class = string.Empty;
            string @style = string.Empty;
            string id = string.Empty;
            string disabled = string.Empty;
            int size = 0;

            CustomListBox customListBox = new CustomListBox();
            customListBox.GetAttributes(ref htmlAttributes, ref @class, ref @style, ref size, ref id, ref disabled, reqFlg);

            MvcHtmlString listBox = SelectExtensions.ListBoxFor(htmlHelper, expression, selectList, htmlAttributes);
            string strHtml = string.Empty;

            strHtml = customListBox.ListBoxForMobile(@class, @style, size, id, selectList, disabled, reqFlg);

            return htmlHelper.Raw(listBox.ToHtmlString() + strHtml);
        }


        public static IHtmlString HtmlEscapeSpecialChars(this HtmlHelper htmlHelper, string content,bool isReplaceSpace = true)
        {
            if (string.IsNullOrEmpty(content))
            {
                return new HtmlString("");
            }
            if (isReplaceSpace)
            {
                return htmlHelper.Raw(HttpUtility.HtmlEncode(content).Replace("\n", "<br>"));
            }
            else
            {
                return htmlHelper.Raw(HttpUtility.HtmlEncode(content));
            }
        }

    }
}

public class CustomListBox
{
    /// <summary>
    /// GetAttributes 
    /// </summary>
    /// <param name="htmlAttributes"></param>
    /// <param name="@class"></param>
    /// <param name="id"></param>
    /// <param name="disabled"></param>
    public void GetAttributes(ref object htmlAttributes, ref string @class, ref string @style, ref int size, ref string id, ref string disabled, bool reqFlg)
    {
         id = htmlAttributes.GetType().GetProperty("id").GetValue(htmlAttributes, null).ToString();
        try
        {
            @class = htmlAttributes.GetType().GetProperty("class").GetValue(htmlAttributes, null).ToString();
        }
        catch { @class = string.Empty; }
        try
        {
            style = htmlAttributes.GetType().GetProperty("style").GetValue(htmlAttributes, null).ToString();
        }
        catch { style = string.Empty; }
        try
        {
            disabled = htmlAttributes.GetType().GetProperty("disabled").GetValue(htmlAttributes, null).ToString();
        }
        catch { disabled = string.Empty; }
        try
        {
            size = int.Parse(htmlAttributes.GetType().GetProperty("size").GetValue(htmlAttributes, null).ToString());
        }
        catch { size = 0; }
        string data_val = string.Empty;
        try
        {
            data_val = htmlAttributes.GetType().GetProperty("data_val").GetValue(htmlAttributes, null).ToString();
        }
        catch { data_val = string.Empty; }
        string data_val_required = string.Empty;
        try
        {
            data_val_required = htmlAttributes.GetType().GetProperty("data_val_required").GetValue(htmlAttributes, null).ToString();
        }
        catch { data_val_required = string.Empty; }
        htmlAttributes = null;
        if (string.IsNullOrEmpty(data_val))
        {
            htmlAttributes = new
            {
                @class = @class,
                id = id,
                style = style + (reqFlg ? "visibility:hidden;height:0px;min-height:0px;width:0px" : "display:none")
            };
        }
        else
        {
            htmlAttributes = new
            {
                @class = @class,
                id = id,
                data_val = data_val,
                data_val_required = data_val_required,
                style = style + (reqFlg ? "visibility:hidden;height:0px;min-height:0px;width:0px" : "display:none")
            };
        }

    }

    /// <summary>
    /// ListBoxForMobile
    /// </summary>
    /// <param name="@class"></param>
    /// <param name="id"></param>
    /// <param name="selectList"></param>
    /// <param name="disabled"></param>
    /// <returns></returns>
    public string ListBoxForMobile(string @class, string css, int size, string id, IEnumerable<SelectListItem> selectList, string disabled, bool reqFlg)
    {
        StringBuilder strHtml = new StringBuilder();
        string @style = string.Empty;
        string[] cSS = css.Split(';');
        for (int i = 0; i < cSS.Length; i++)
        {
            if (cSS[i].ToLower().Contains("width"))
            {
                @style += cSS[i] + ";";
            }
            if (cSS[i].ToLower().Contains("padding"))
            {
                @style += cSS[i] + ";";
            }
        }
        if (size>0)
        {
            @style += "height:" + (19 * size)+"px;";
        }
        else
        {
            for (int i = 0; i < cSS.Length; i++)
            {
                if (cSS[i].ToLower().Contains("height"))
                {
                    @style += cSS[i] + ";";
                }
            }
        }
        if (reqFlg)
        {
            @style += "margin-top:-17px;";
        }
        if (@class.Contains("textarea"))
        {
            @class = @class.Replace("textarea", string.Empty);
        }
        strHtml.AppendLine("<div class='custom-list-box " + @class + "' id='dv_" + id + "' data-trigger-key='" + id + "' style='" + @style + "'>");
        int icount = 0;
        foreach (var item in selectList)
        {
            strHtml.AppendLine("<div class='dv_item_" + id + " custom-item-list-box'");
            strHtml.AppendLine(" data-item='" + icount + "' data-status='false' data-key='" + item.Value + "' ");
            strHtml.AppendLine(" style='background-color:" + (item.Selected ? "#3399ff" : "rgb(255, 255, 255)") + ";'>");
            strHtml.AppendLine("    <label style='color:" + (item.Selected ? "white" : "black") + "'>" + HttpUtility.HtmlEncode(item.Text) + "</label>");
            strHtml.AppendLine("</div>");
            icount++;
        }
        strHtml.AppendLine("</div>");
        strHtml.AppendLine("<script>");
        strHtml.AppendLine("$(function() {");
        strHtml.AppendLine("    var isScroll_" + id + " = false;");
        strHtml.AppendLine("    var is_ie = navigator.userAgent.indexOf('MSIE') > -1 || navigator.userAgent.indexOf('Trident/') > -1;");
        strHtml.AppendLine("    var clicking_" + id + " = false;");
        strHtml.AppendLine("    var currentTop_" + id + " = 0;");
        strHtml.AppendLine("    var current_" + id + " = 0;");
        strHtml.AppendLine("    var imove_first_" + id + " = 0;");
        strHtml.AppendLine("    var auto" + id + " = null;");
        strHtml.AppendLine("    var isClick_" + id + " = true;");
        strHtml.AppendLine("    var timer_" + id + ";");
        strHtml.AppendLine("    var touchstart_" + id + " = false;");
        strHtml.AppendLine("    var selected_" + id + " = false;");
        strHtml.AppendLine("    if (is_ie){");
        strHtml.AppendLine("        $('#dv_" + id + "').scroll(function(event){");
        strHtml.AppendLine("            event.stopPropagation();");
        strHtml.AppendLine("            event.preventDefault();");
        strHtml.AppendLine("            isScroll_" + id + " = true;");
        strHtml.AppendLine("        });");
        strHtml.AppendLine("    }");
        strHtml.AppendLine("    $('#dv_" + id + "').bind('click mousedown touchstart', function (event) {");
        strHtml.AppendLine("        event.stopPropagation();");
        strHtml.AppendLine("        $('.custom-list-box').each(function() {");
        strHtml.AppendLine("            $(this).css('border','1px solid rgb(211, 214, 219)');");
        strHtml.AppendLine("        });");
        strHtml.AppendLine("        $('.custom-item-list-box').each(function() {");
        strHtml.AppendLine("            if ($(this).css('background-color') != 'rgb(255, 255, 255)'){");
        strHtml.AppendLine("                $(this).css('background-color','rgb(211, 214, 219)');");
        strHtml.AppendLine("                $(this).find('label').css('color', 'black');");
        strHtml.AppendLine("            }");
        strHtml.AppendLine("        });");
        strHtml.AppendLine("        $(this).css('border','1px solid #3399ff');");
        strHtml.AppendLine("        $('.dv_item_" + id + "').each(function() {");
        strHtml.AppendLine("             if ($(this).css('background-color') != 'rgb(255, 255, 255)'){");
        strHtml.AppendLine("                $(this).css('background-color','#3399ff');");
        strHtml.AppendLine("                $(this).find('label').css('color', 'white');");
        strHtml.AppendLine("             }");
        strHtml.AppendLine("        });");
        strHtml.AppendLine("        if(isScroll_" + id + " == false && is_ie) {");
        strHtml.AppendLine("            $('#dv_" + id + "').trigger('mouseup')");
        strHtml.AppendLine("        }");
        strHtml.AppendLine("    });");
        strHtml.AppendLine("    $(window).click(function() {");
        strHtml.AppendLine("        $('#dv_" + id + "').css('border','1px solid rgb(211, 214, 219)')");
        strHtml.AppendLine("        $('.dv_item_" + id + "').each(function() {");
        strHtml.AppendLine("            if ($(this).css('background-color') != 'rgb(255, 255, 255)'){");
        strHtml.AppendLine("                $(this).css('background-color','rgb(211, 214, 219)');");
        strHtml.AppendLine("                $(this).find('label').css('color', 'black');");
        strHtml.AppendLine("            }");
        strHtml.AppendLine("        });");
        strHtml.AppendLine("    });");
        strHtml.AppendLine("    $(window).trigger('click');");
        strHtml.AppendLine("    $('#" + id + "').change(function() {");
        strHtml.AppendLine("        $('#dv_" + id + "').html('');");
        strHtml.AppendLine("        $('#dv_" + id + "').unbind('mousedown');");
        strHtml.AppendLine("        $('#dv_" + id + "').unbind('mouseup');");
        strHtml.AppendLine("        $('#dv_" + id + "').unbind('mousemove');");
        strHtml.AppendLine("        $('.dv_item_" + id + "').unbind('click');");
        strHtml.AppendLine("        var strHtml = '';");
        strHtml.AppendLine("        var il = document.getElementById('" + id + "');");
        strHtml.AppendLine("        for (var i = 0; i < il.length; i++)");
        strHtml.AppendLine("        {");
        strHtml.AppendLine("            strHtml += '<div class=\"dv_item_" + id + " custom-item-list-box\"';");
        strHtml.AppendLine("            strHtml += ' data-item=\"' + i + '\" data-status=\"false\" data-key=\"'+ il.options[i].value +'\" ';");
        strHtml.AppendLine("            strHtml += ' style=\"background-color:'+(il.options[i].selected ? '#3399ff':'rgb(255, 255, 255)') + ';\">'; ");
        strHtml.AppendLine("            strHtml += '     <label style=\"color:'+ (il.options[i].selected ? 'white' : 'black') +'\">' + escapeSpecialChars(il.options[i].text) + '</label>'; ");
        strHtml.AppendLine("            strHtml += '</div>';");
        strHtml.AppendLine("        }");
        strHtml.AppendLine("        $('#dv_" + id + "').html(strHtml);");
        strHtml.AppendLine("        if (is_ie){");
        strHtml.AppendLine("            $('.dv_item_" + id + "').mousedown(function(event){");
        strHtml.AppendLine("                event.preventDefault();");
        strHtml.AppendLine("                isScroll_" + id + " = true;");
        strHtml.AppendLine("                clicking_" + id + " = true;");
        strHtml.AppendLine("            });");
        strHtml.AppendLine("        }");
        strHtml.AppendLine("        $('.dv_item_" + id + "').bind('click', function(event) {");
        strHtml.AppendLine("            if (!isClick_" + id + " || touchstart_" + id + " ) {touchstart_" + id + "= false; return;}");
        strHtml.AppendLine("            var currentItem = $(this).attr('data-item');");
        strHtml.AppendLine("            var key = $(this).attr('data-key');");
        strHtml.AppendLine("            var trigger_key = $(this).parent().attr('data-trigger-key');");
        strHtml.AppendLine("            if(! /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent) ) {");
        strHtml.AppendLine("                var selectedItem = $('.dv_item_" + id + "[data-status=\"true\"]').attr('data-item');");
        strHtml.AppendLine("                if (selectedItem == undefined) {");
        strHtml.AppendLine("                    selectedItem = currentItem;");
        strHtml.AppendLine("                }");
        strHtml.AppendLine("                $('.dv_item_" + id + "').each(function() {");
        strHtml.AppendLine("                    $(this).attr('data-status','false');");
        strHtml.AppendLine("                });");
        strHtml.AppendLine("                //Event shift click");
        strHtml.AppendLine("                if (event.shiftKey) {");
        strHtml.AppendLine("                    //Clear Selected");
        strHtml.AppendLine("                    $('.dv_item_" + id + "').each(function() {");
        strHtml.AppendLine("                        $(this).css('background-color', 'rgb(255, 255, 255)');");
        strHtml.AppendLine("                        $(this).find('label').css('color', 'black');");
        strHtml.AppendLine("                    });");
        strHtml.AppendLine("                    $('.dv_item_" + id + "').each(function() {");
        strHtml.AppendLine("                        var item = parseInt($(this).attr('data-item'));");
        strHtml.AppendLine("                        if ((item >= parseInt(currentItem) && item <= parseInt(selectedItem))");
        strHtml.AppendLine("                            || (item <= parseInt(currentItem) && item >= parseInt(selectedItem))) {");
        strHtml.AppendLine("                            $(this).css('background-color', '#3399ff');");
        strHtml.AppendLine("                            $(this).find('label').css('color', 'white');");
        strHtml.AppendLine("                            if (item == selectedItem) {");
        strHtml.AppendLine("                                $(this).attr('data-status','true');");
        strHtml.AppendLine("                            }");
        strHtml.AppendLine("                        }");
        strHtml.AppendLine("                    });");
        strHtml.AppendLine("                }else{");
        strHtml.AppendLine("                    if ($(this).css('background-color') == 'rgb(255, 255, 255)') {");
        strHtml.AppendLine("                        $(this).css('background-color', '#3399ff');");
        strHtml.AppendLine("                        $(this).find('label').css('color', 'white');");
        strHtml.AppendLine("                    }else {");
        strHtml.AppendLine("                        $(this).css('background-color', 'rgb(255, 255, 255)');");
        strHtml.AppendLine("                        $(this).find('label').css('color', 'black');");
        strHtml.AppendLine("                    }");
        strHtml.AppendLine("                    $(this).attr('data-status','true');");
        strHtml.AppendLine("                }");
        strHtml.AppendLine("                $('.dv_item_" + id + "').each(function() {");
        strHtml.AppendLine("                    $('#' + trigger_key + ' option[value=\"' + $(this).attr('data-key') + '\"]')[0].selected = ");
        strHtml.AppendLine("                     $(this).css('background-color') == 'rgb(255, 255, 255)'? false : true;");
        strHtml.AppendLine("                });");
        strHtml.AppendLine("            }else{");
        strHtml.AppendLine("                if ($(this).css('background-color') == 'rgb(255, 255, 255)'){");
        strHtml.AppendLine("                    $(this).css('background-color', '#3399ff');");
        strHtml.AppendLine("                    $(this).find('label').css('color', 'white');");
        strHtml.AppendLine("                    $('#' + trigger_key + ' option[value=\"' + key + '\"]')[0].selected = true;");
        strHtml.AppendLine("                } else {");
        strHtml.AppendLine("                    $(this).css('background-color', 'rgb(255, 255, 255)');");
        strHtml.AppendLine("                    $(this).find('label').css('color', 'black');");
        strHtml.AppendLine("                    $('#' + trigger_key + ' option[value=\"' + key + '\"]')[0].selected = false;");
        strHtml.AppendLine("                }");
        strHtml.AppendLine("            }");
        strHtml.AppendLine("        });");
        strHtml.AppendLine("        $('#dv_" + id + "').mousedown(function(event){");
        strHtml.AppendLine("            event.preventDefault();");
        strHtml.AppendLine("            event.stopPropagation();");
        strHtml.AppendLine("            clicking_" + id + " = true;");
        strHtml.AppendLine("            currentTop_" + id + " = event.pageY + $(this).scrollTop();");
        strHtml.AppendLine("            current_" + id + " =  event.pageY;");
        strHtml.AppendLine("            imove_first_" + id + " = 1;");
        strHtml.AppendLine("            if(isScroll_" + id + " == false && is_ie) {");
        strHtml.AppendLine("                $('#dv_" + id + "').trigger('mouseup')");
        strHtml.AppendLine("            }");
        strHtml.AppendLine("        });");
        strHtml.AppendLine("        $('#dv_" + id + "').mouseup(function() {");
        strHtml.AppendLine("            clicking_" + id + " = false;");
        strHtml.AppendLine("            isScroll_" + id + " = false;");
        strHtml.AppendLine("            imove_first_" + id + " = 0;");
        strHtml.AppendLine("            auto" + id + " = setInterval(function () {");
        strHtml.AppendLine("                isClick_" + id + " = true;");
        strHtml.AppendLine("                clearInterval(auto" + id + ");");
        strHtml.AppendLine("            }, 200);");
        strHtml.AppendLine("        });");
        strHtml.AppendLine("        $(document).mouseup(function(event) {");
        strHtml.AppendLine("            clicking_" + id + " = false;");
        strHtml.AppendLine("            isScroll_" + id + " = false;");
        strHtml.AppendLine("        });");
        strHtml.AppendLine("        $('#dv_" + id + "').mousemove(function(event){");
        strHtml.AppendLine("            var scrollTop = $(this).scrollTop();");
        strHtml.AppendLine("            if(clicking_" + id + " === false) { isScroll_" + id + " = false; return;}");
        strHtml.AppendLine("            if (currentTop_" + id + " === event.pageY + scrollTop && imove_first_" + id + " === 1) {isScroll_" + id + " = false; return;}");
        strHtml.AppendLine("            if(isScroll_" + id + " === false && is_ie) return;");
        strHtml.AppendLine("            var diff = event.pageY > currentTop_" + id + " ? -12: 0;");
        strHtml.AppendLine("            diff = diff - scrollTop;");
        strHtml.AppendLine("            $('.dv_item_" + id + "').each(function() {");
        strHtml.AppendLine("                 var top = $(this).offset().top;");
        strHtml.AppendLine("                 if ((parseInt(top)>=parseInt(currentTop_" + id + "+diff) && parseInt(top)<=parseInt(event.pageY))");
        strHtml.AppendLine("                    || (parseInt(top)<=parseInt(currentTop_" + id + "+diff) && parseInt(top)>=parseInt(event.pageY))) {");
        strHtml.AppendLine("                    $(this).css('background-color', '#3399ff');");
        strHtml.AppendLine("                    $(this).find('label').css('color', 'white');");
        strHtml.AppendLine("                    $('#" + id + " option[value=\"' + $(this).attr('data-key') + '\"]')[0].selected = true;");
        strHtml.AppendLine("                 }else{");
        strHtml.AppendLine("                    $(this).css('background-color', 'rgb(255, 255, 255)');");
        strHtml.AppendLine("                    $(this).find('label').css('color', 'black');");
        strHtml.AppendLine("                    $('#" + id + " option[value=\"' + $(this).attr('data-key') + '\"]')[0].selected = false;");
        strHtml.AppendLine("                 }");
        strHtml.AppendLine("            });");
        strHtml.AppendLine("            if (current_" + id + " > event.pageY){");
        strHtml.AppendLine("                $(this).scrollTop(scrollTop - 5)");
        strHtml.AppendLine("            }else{");
        strHtml.AppendLine("                $(this).scrollTop(scrollTop + 5)");
        strHtml.AppendLine("            }");
        strHtml.AppendLine("            imove_first_" + id + "++;");
        strHtml.AppendLine("            isClick_" + id + " = false;");
        strHtml.AppendLine("            current_" + id + " = event.pageY;");
        strHtml.AppendLine("        });");
        strHtml.AppendLine("        $('.dv_item_" + id + "').bind('touchstart', function() {");
        strHtml.AppendLine("            clearTimeout(timer_" + id + ");");
        strHtml.AppendLine("            if ($(this).css('background-color') == 'rgb(255, 255, 255)'){");
        strHtml.AppendLine("                selected_" + id + " = true;");
        strHtml.AppendLine("            }else{");
        strHtml.AppendLine("                selected_" + id + " = false;");
        strHtml.AppendLine("            }");
        strHtml.AppendLine("            timer_" + id + " = setTimeout(function() {");
        strHtml.AppendLine("                touchstart_" + id + " = true; ");
        strHtml.AppendLine("                $('.dv_item_" + id + "').each(function() {");
        strHtml.AppendLine("                    var key = $(this).attr('data-key');");
        strHtml.AppendLine("                    var trigger_key = $(this).parent().attr('data-trigger-key');");
        strHtml.AppendLine("                    if (selected_" + id + "){");
        strHtml.AppendLine("                        $(this).css('background-color', '#3399ff');");
        strHtml.AppendLine("                        $(this).find('label').css('color', 'white');");
        strHtml.AppendLine("                        $('#' + trigger_key + ' option[value=\"' + key + '\"]')[0].selected = true;");
        strHtml.AppendLine("                    }else{");
        strHtml.AppendLine("                        $(this).css('background-color', 'rgb(255, 255, 255)');");
        strHtml.AppendLine("                        $(this).find('label').css('color', 'black');");
        strHtml.AppendLine("                        $('#" + id + " option[value=\"' + $(this).attr('data-key') + '\"]')[0].selected = false;");
        strHtml.AppendLine("                    }");
        strHtml.AppendLine("                });");
        strHtml.AppendLine("            },1000);");
        strHtml.AppendLine("        }).bind('touchend touchmove', function () {");
        strHtml.AppendLine("            clearTimeout(timer_" + id + ");");
        strHtml.AppendLine("        });");
        strHtml.AppendLine("    });");
        strHtml.AppendLine("    $('#dv_" + id + "').mousedown(function(event){");
        strHtml.AppendLine("        event.preventDefault();");
        strHtml.AppendLine("        clicking_" + id + " = true;");
        strHtml.AppendLine("        currentTop_" + id + " = event.pageY + $(this).scrollTop();");
        strHtml.AppendLine("        current_" + id + " =  event.pageY;");
        strHtml.AppendLine("        imove_first_" + id + " = 1;");
        strHtml.AppendLine("        if(isScroll_" + id + " == false && is_ie) {");
        strHtml.AppendLine("            $('#dv_" + id + "').trigger('mouseup')");
        strHtml.AppendLine("        }");
        strHtml.AppendLine("    });");
        strHtml.AppendLine("    $('#dv_" + id + "').mouseup(function() {");
        strHtml.AppendLine("        clicking_" + id + " = false;");
        strHtml.AppendLine("        isScroll_" + id + " = false;");
        strHtml.AppendLine("        imove_first_" + id + " = 0;");
        strHtml.AppendLine("        auto" + id + " = setInterval(function () {");
        strHtml.AppendLine("            isClick_" + id + " = true;");
        strHtml.AppendLine("            clearInterval(auto" + id + ");");
        strHtml.AppendLine("        }, 200);");
        strHtml.AppendLine("    });");
        strHtml.AppendLine("    $(document).mouseup(function(event) {");
        strHtml.AppendLine("        clicking_" + id + " = false;");
        strHtml.AppendLine("        isScroll_" + id + " = false;");
        strHtml.AppendLine("    });");
        strHtml.AppendLine("    $('#dv_" + id + "').mousemove(function(event){");
        strHtml.AppendLine("        var scrollTop = $(this).scrollTop();");
        strHtml.AppendLine("        if(clicking_" + id + " === false) {isScroll_" + id + " = false; return;}");
        strHtml.AppendLine("        if (currentTop_" + id + " === event.pageY + scrollTop && imove_first_" + id + " === 1) {isScroll_" + id + " = false; return;}");
        strHtml.AppendLine("        if(isScroll_" + id + " === false && is_ie) return;");
        strHtml.AppendLine("        var diff = event.pageY > currentTop_" + id + " ? -12: 0;");
        strHtml.AppendLine("        diff = diff - scrollTop;");
        strHtml.AppendLine("        $('.dv_item_" + id + "').each(function() {");
        strHtml.AppendLine("             var top = $(this).offset().top;");
        strHtml.AppendLine("             if ((parseInt(top)>=parseInt(currentTop_" + id + "+diff) && parseInt(top)<=parseInt(event.pageY))");
        strHtml.AppendLine("                || (parseInt(top)<=parseInt(currentTop_" + id + "+diff) && parseInt(top)>=parseInt(event.pageY))) {");
        strHtml.AppendLine("                $(this).css('background-color', '#3399ff');");
        strHtml.AppendLine("                $(this).find('label').css('color', 'white');");
        strHtml.AppendLine("                $('#" + id + " option[value=\"' + $(this).attr('data-key') + '\"]')[0].selected = true;");
        strHtml.AppendLine("             }else{");
        strHtml.AppendLine("                $(this).css('background-color', 'rgb(255, 255, 255)');");
        strHtml.AppendLine("                $(this).find('label').css('color', 'black');");
        strHtml.AppendLine("                $('#" + id + " option[value=\"' + $(this).attr('data-key') + '\"]')[0].selected = false;");
        strHtml.AppendLine("             }");
        strHtml.AppendLine("        });");
        strHtml.AppendLine("        if (current_" + id + " > event.pageY){");
        strHtml.AppendLine("            $(this).scrollTop(scrollTop - 5)");
        strHtml.AppendLine("        }else{");
        strHtml.AppendLine("            $(this).scrollTop(scrollTop + 5)");
        strHtml.AppendLine("        }");
        strHtml.AppendLine("        imove_first_" + id + "++;");
        strHtml.AppendLine("        isClick_" + id + " = false;");
        strHtml.AppendLine("        current_" + id + " = event.pageY;");
        strHtml.AppendLine("    });");
        strHtml.AppendLine("    $('.dv_item_" + id + "').bind('click', function(event) {");
        strHtml.AppendLine("        if (!isClick_" + id + " || touchstart_" + id + " ) {touchstart_" + id + "= false; return;}");
        strHtml.AppendLine("        var currentItem = $(this).attr('data-item');");
        strHtml.AppendLine("        var key = $(this).attr('data-key');");
        strHtml.AppendLine("        var trigger_key = $(this).parent().attr('data-trigger-key');");
        strHtml.AppendLine("        if(! /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent) ) {");
        strHtml.AppendLine("            var selectedItem = $('.dv_item_" + id + "[data-status=\"true\"]').attr('data-item');");
        strHtml.AppendLine("            if (selectedItem == undefined) {");
        strHtml.AppendLine("                selectedItem = currentItem;");
        strHtml.AppendLine("            }");
        strHtml.AppendLine("            $('.dv_item_" + id + "').each(function() {");
        strHtml.AppendLine("                $(this).attr('data-status','false');");
        strHtml.AppendLine("            });");
        strHtml.AppendLine("            //Event shift click");
        strHtml.AppendLine("            if (event.shiftKey) {");
        strHtml.AppendLine("                //Clear Selected");
        strHtml.AppendLine("                $('.dv_item_" + id + "').each(function() {");
        strHtml.AppendLine("                    $(this).css('background-color', 'rgb(255, 255, 255)');");
        strHtml.AppendLine("                    $(this).find('label').css('color', 'black');");
        strHtml.AppendLine("                });");
        strHtml.AppendLine("                $('.dv_item_" + id + "').each(function() {");
        strHtml.AppendLine("                    var item = parseInt($(this).attr('data-item'));");
        strHtml.AppendLine("                    if ((item >= parseInt(currentItem) && item <= parseInt(selectedItem))");
        strHtml.AppendLine("                        || (item <= parseInt(currentItem) && item >= parseInt(selectedItem))) {");
        strHtml.AppendLine("                        $(this).css('background-color', '#3399ff');");
        strHtml.AppendLine("                        $(this).find('label').css('color', 'white');");
        strHtml.AppendLine("                        if (item == selectedItem) {");
        strHtml.AppendLine("                            $(this).attr('data-status','true');");
        strHtml.AppendLine("                        }");
        strHtml.AppendLine("                    }");
        strHtml.AppendLine("                });");
        strHtml.AppendLine("            }else{");
        strHtml.AppendLine("                if ($(this).css('background-color') == 'rgb(255, 255, 255)') {");
        strHtml.AppendLine("                    $(this).css('background-color', '#3399ff');");
        strHtml.AppendLine("                    $(this).find('label').css('color', 'white');");
        strHtml.AppendLine("                }else {");
        strHtml.AppendLine("                    $(this).css('background-color', 'rgb(255, 255, 255)');");
        strHtml.AppendLine("                    $(this).find('label').css('color', 'black');");
        strHtml.AppendLine("                }");
        strHtml.AppendLine("                $(this).attr('data-status','true');");
        strHtml.AppendLine("            }");
        strHtml.AppendLine("            $('.dv_item_" + id + "').each(function() {");
        strHtml.AppendLine("                $('#' + trigger_key + ' option[value=\"' + $(this).attr('data-key') + '\"]')[0].selected = ");
        strHtml.AppendLine("                    $(this).css('background-color') == 'rgb(255, 255, 255)'? false : true;");
        strHtml.AppendLine("            });");
        strHtml.AppendLine("        }else{");
        strHtml.AppendLine("            if ($(this).css('background-color') == 'rgb(255, 255, 255)'){");
        strHtml.AppendLine("                $(this).css('background-color', '#3399ff');");
        strHtml.AppendLine("                $(this).find('label').css('color', 'white');");
        strHtml.AppendLine("                $('#' + trigger_key + ' option[value=\"' + key + '\"]')[0].selected = true;");
        strHtml.AppendLine("            } else {");
        strHtml.AppendLine("                $(this).css('background-color', 'rgb(255, 255, 255)');");
        strHtml.AppendLine("                $(this).find('label').css('color', 'black');");
        strHtml.AppendLine("                $('#' + trigger_key + ' option[value=\"' + key + '\"]')[0].selected = false;");
        strHtml.AppendLine("            }");
        strHtml.AppendLine("        }");
        strHtml.AppendLine("    });");
        strHtml.AppendLine("    $('.dv_item_" + id + "').bind('touchstart', function(event) {");
        strHtml.AppendLine("        clearTimeout(timer_" + id + ");");
        strHtml.AppendLine("        if ($(this).css('background-color') == 'rgb(255, 255, 255)'){");
        strHtml.AppendLine("            selected_" + id + " = true;");
        strHtml.AppendLine("        }else{");
        strHtml.AppendLine("            selected_" + id + " = false;");
        strHtml.AppendLine("        }");
        strHtml.AppendLine("        timer_" + id + " = setTimeout(function() {"); 
        strHtml.AppendLine("            touchstart_" + id + " = true; ");
        strHtml.AppendLine("            $('.dv_item_" + id + "').each(function() {");
        strHtml.AppendLine("                var key = $(this).attr('data-key');");
        strHtml.AppendLine("                var trigger_key = $(this).parent().attr('data-trigger-key');");
        strHtml.AppendLine("                if (selected_" + id + "){");
        strHtml.AppendLine("                    $(this).css('background-color', '#3399ff');");
        strHtml.AppendLine("                    $(this).find('label').css('color', 'white');");
        strHtml.AppendLine("                    $('#' + trigger_key + ' option[value=\"' + key + '\"]')[0].selected = true;");
        strHtml.AppendLine("                }else{");
        strHtml.AppendLine("                    $(this).css('background-color', 'rgb(255, 255, 255)');");
        strHtml.AppendLine("                    $(this).find('label').css('color', 'black');");
        strHtml.AppendLine("                    $('#" + id + " option[value=\"' + $(this).attr('data-key') + '\"]')[0].selected = false;");
        strHtml.AppendLine("                }");
        strHtml.AppendLine("            });");
        strHtml.AppendLine("        },1000);");
        strHtml.AppendLine("    }).bind('touchend touchmove', function () {");
        strHtml.AppendLine("        clearTimeout(timer_" + id + ");");
        strHtml.AppendLine("    });");
        strHtml.AppendLine("});");
        strHtml.AppendLine("</script>");
        return strHtml.ToString();
    }
}

public class HtmlHelpers
{

}

public class Calendar
{
    public string MiniDayRender(DateTime dtYmd, bool flg, bool sunflg,string pickDate)
    {
        StringBuilder html = new StringBuilder();
        bool flgCheck = false;
        if (pickDate.Length>0)
        {
            if (pickDate.Contains(dtYmd.ToString("yyyy/MM/dd")))
            {
                flgCheck = true;
            }
        }
        html.AppendLine("<td class=\"has-text-centered " + (sunflg ? "sunColor" : string.Empty) + "\">");
        html.AppendLine("   <input class=\"checkbox\" id=\"cb_" + (!flg ? "1" : "2") + "_" + dtYmd.Day.ToString("00") + "\" name=\"cbDate\" data-date=\"" + dtYmd.ToString("yyyy/MM/dd") + "\" type=\"checkbox\" "+(flgCheck?"checked":string.Empty)+">");
        html.AppendLine("   <label for=\"cb_" + (!flg ? "1" : "2") + "_" + dtYmd.Day.ToString("00") + "\"> " + dtYmd.Day.ToString("00") + "</label>");
        html.AppendLine("</td>");
        return html.ToString();
    }

    public string DateRender(string ym)
    {
        var str = new StringBuilder();
        #region Header
        str.AppendLine("<table style=\"margin-bottom:5px\">");
        str.AppendLine("    <tr>");
        str.AppendLine("    <td style=\"width:10px\">");
        str.AppendLine("        <a href =\"javascript:void(0);\" onclick =\"onNextPressMonth('" + (ym + "/01") + "',0)\"><i class=\"fa fa-chevron-circle-left\" style=\"font-size:25px;\"></i></a>");
        str.AppendLine("    </td>");
        str.AppendLine("    <td class=\"has-text-centered\" style=\"vertical-align:middle\">");
        str.AppendLine(ym);
        str.AppendLine("    </td>");
        str.AppendLine("    </tr>");
        str.AppendLine("</table>");
        #endregion
        return str.ToString();
    }
}

public static class DateTimeExtensions
{
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
        int diff = dt.DayOfWeek - startOfWeek;
        if (diff < 0)
        {
            diff += 7;
        }
        return dt.AddDays(-1 * diff).Date;
    }
}

public static class JavascriptOrCssExtension
{
    public static MvcHtmlString IncludeVersioned(this HtmlHelper htmlHelper, string path)
    {
        string version = ConfigurationManager.AppSettings["VersionKey"].ToString();
        if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(version))
        {
            if (path.ToLower().Contains(".js"))
            {
                return MvcHtmlString.Create("<script type='text/javascript' src='" + path + "?vs=" + version + "'></script>");
            }
            else
            {
                return MvcHtmlString.Create("<link rel='stylesheet' href='" + path + "?vs=" + version + "' />");
            }
        }
        return new MvcHtmlString(string.Empty);
    }
}