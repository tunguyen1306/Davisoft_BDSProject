using System.Web.Optimization;
using NS;
using NS.Mvc.Helpers;

namespace Davisoft_BDSProject.Web
{
    public class 
        BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/display/css/min").Include(
                "~/Display/css/OpenSans.css"
                , "~/Display/css/bootstrap.min.css"
                , "~/Display/css/bootstrap-theme.css" // tooltips
                , "~/Display/css/jquery.handsontable.full.css" // tooltips for sidebar on touch device

                , "~/Display/js/selectize-0.12.1/css/selectize.css" // colorbox
                , "~/Display/js/selectize-0.12.1/css/selectize.bootstrap3.css" // colorbox
                //, "~/Display/js/selectize-0.12.1/less/plugins/drag_drop.less" // colorbox

                , "~/Display/js/select2-4.0.1/css/select2.css" // colorbox

                , "~/Display/js/flag-icon-css/flag-icon.css"
                , "~/Display/css/bd.css" // code prettify
                , "~/Display/css/style.css" // sticky notifications
                , "~/Display/css/buttons.css" // aditional icons
                , "~/Display/css/app.css" // flags
                , "~/Display/css/font-awesome.css" // datatables
                , "~/Display/css/database.css" // datatables
                , "~/Display/css/fixedHeader.dataTables.min.css" // fixed header datatables
                , "~/Display/js/chosen/chosen.css"
                //, "~/Display/js/datepicker/datepicker.css" // color picker
                , "~/Display/js/colorpicker/css/bootstrap-colorpicker.css"
                , "~/Display/css/bootstrap-multiselect.css"
                , "~/Display/css/jquery-ui.min.css"
                //, "~/Display/css/thickbox.css"
                , "~/Display/js/magnific-popup/magnific-popup.css"
                //, "~/Display/js/material-datetimepicker/css/bootstrap-material-datetimepicker.css"
                , "~/Display/js/datepicker/datepicker.css"
                , "~/Display/js/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css"
                //, "~/Display/js/bootstrap-material-design/dist/css/bootstrap-material-design.min.css"
                , "~/Display/lib/DataTables-1.10.0/extensions/FixedColumns/css/dataTables.fixedColumns.css"
                , "~/Display/lib/multi-select/css/multi-select.css"
                ));
            bundles.Add(new CssBundle("~/display/css/print").Include(
                    "~/Display/css/print.css"
                ));

            bundles.Add(new JsBundle("~/content/js/min").Include(
                "~/Display/js/modernizr.js"
                , "~/Display/js/jquery-2.1.4.js"
                , "~/Display/js/moment.js"
                , "~/Display/js/bootstrap.min.js" // easing plugin
                , "~/Display/js/jquery-ui.min.js" // smart resize event
                , "~/Display/js/main.js" // js cookie plugin
                , "~/Display/js/datatable/datatable.js" // main bootstrap js
                , "~/Display/js/datatable/dataTables.fixedHeader.min.js" // main bootstrap js
                //, "~/Display/js/datatable/dataTables.buttons.min.js" // main bootstrap js

                //, "~/Display/js/datatable/datatable_min.js"
                , "~/Display/js/chosen/chosen.jquery.min.js"
                //, "~/Display/js/datepicker/bootstrap-datepicker.js" // sticky messages
                //, "~/Display/js/bootstrap-datetimepicker.min.js" // tooltips
                , "~/Display/js/colorpicker/js/bootstrap-colorpicker.js" // jBreadcrumbs
                , "~/Display/js/app.plugin.js" // hidden elements width/height
                , "~/Display/js/jquery.custom-file-input.js" // scroll
                , "~/Display/js/jquery.handsontable.full.js"
                //, "~/Display/js/select2.full.js" // fix for ios orientation change
                , "~/Display/js/select2-4.0.1/js/select2.full.js" // fix for ios orientation change

                , "~/Display/js/selectize-0.12.1/js/standalone/selectize.js"


                , "~/Display/js/bs.pagination.js" // to top
                , "~/Display/js/bootstrap-multiselect.js"
                , "~/Display/js/jquery.menu-aim.js"
                , "~/Display/js/highchart/highcharts.js"
                , "~/Display/js/highchart/highcharts-more.js"
                , "~/Display/js/highchart/grouped-categories.js"
                , "~/Display/js/highchart/modules/exporting.js" // datatable
                , "~/Display/js/highchart/modules/no-data-to-display.js" // datatable
                , "~/Display/js/thickbox.js"
                , "~/Display/js/magnific-popup/jquery.magnific-popup.js"
                //, "~/Display/js/material-datetimepicker/js/bootstrap-material-datetimepicker.js"
                //, "~/Display/js/bootstrap-material-design/dist/js/material.min.js"
                , "~/Display/js/ultilities/LINQ_JS.js"
                , "~/Display/js/ultilities/string.js"
                , "~/Display/js/uploadify/jquery.uploadify.js"
                , "~/Display/js/accounting/accounting.js"
                , "~/Display/js/bootbox/bootbox.min.js"
                , "~/Scripts/jQuery.tmpl.js"
                , "~/Scripts/jquery.validate.js"
                , "~/Scripts/jquery.validate.unobtrusive.js"
                , "~/Scripts/jquery.unobtrusive-ajax.js"
                , "~/Display/lib/DataTables-1.10.0/extensions/FixedColumns/js/dataTables.fixedColumns.js"
                , "~/Scripts/multi-select/jquery.multi-select.js"
                , "~/Scripts/multi-select/jquery.quicksearch.js"
                , "~/Display/js/datepicker/bootstrap-datepicker.js"
                , "~/Display/js/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"
                , "~/Display/js/cldrjs-0.4.4/dist/cldr.js"
                , "~/Display/js/cldrjs-0.4.4/dist/cldr/event.js"
                , "~/Display/js/cldrjs-0.4.4/dist/cldr/supplemental.js"
                , "~/Display/js/globalize-1.1.1/dist/globalize.js"
                , "~/Display/js/globalize-1.1.1/dist/globalize/number.js"
                , "~/Display/js/globalize-1.1.1/dist/globalize/date.js"
                , "~/Display/js/jquery.price_format.2.0.js"
                ));


            bundles.Add(new CssBundle("~/css/ie8").Include("~/Content/css/ie.css"));

            bundles.Add(new JsBundle("~/js/ie9").Include(
                "~/Content/js/ie/html5.js"
                , "~/Content/js/ie/respond.min.js"
                , "~/Content/lib/flot/excanvas.min.js"));
        }
    }
}
