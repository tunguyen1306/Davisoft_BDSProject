(function ($) {

    $.fn.timeview = function (options) {

        // Create some defaults, extending them with any options that were provided
        var settings = $.extend({
            "data": "",
            "hourinterval": 1,
            "mininterval": 5,
            "containerpx": 1200,
            "minintervalpx": 3,
            "actualmintervalpx": 3,
            "cellpaddingleftright": 6, //it is needed as css apply padding:3px for each cell
            "title": "Timeline Chart",
            "leftth": [
                { "width": 25, "align": "center", "rowspan": 2, "text": "No", "index": "no" },
                { "width": 120, "align": "center", "rowspan": 2, "text": "Team", "index": "team" },
                { "width": 120, "align": "center", "rowspan": 2, "text": "Driver", "index": "name" },
                { "width": 50, "align": "center", "rowspan": 0, "text": "Type", "index": "code" },
            ],
            "hours": [
                { "start": "0600", "end": "0700" },
                { "start": "0700", "end": "0800" },
                { "start": "0800", "end": "0900" },
                { "start": "0900", "end": "1000" },
                { "start": "1000", "end": "1100" },
                { "start": "1100", "end": "1200" },
                { "start": "1200", "end": "1300" },
                { "start": "1300", "end": "1400" },
                { "start": "1400", "end": "1500" },
                { "start": "1500", "end": "1600" },
                { "start": "1600", "end": "1700" },
                { "start": "1700", "end": "1800" },
                { "start": "1800", "end": "1900" },
                { "start": "1900", "end": "2000" },
                { "start": "2000", "end": "2100" },
                { "start": "2100", "end": "2200" },
                { "start": "2200", "end": "2300" },
                { "start": "2300", "end": "0000" },
                { "start": "0000", "end": "0100" },
                { "start": "0100", "end": "0200" },
                { "start": "0200", "end": "0300" },
                { "start": "0300", "end": "0400" },
                { "start": "0400", "end": "0500" },
                { "start": "0500", "end": "0600" }
             ]
        }, options);

        var cells = ((settings.hourinterval * 60) / settings.mininterval);

        var addon = { "hourintervalpx": cells * settings.minintervalpx,
            "hourintervalcolspan": cells,
            "mincells": cells
        };

        var settings = $.extend(settings, addon);

        return this.each(function () {


            // Tooltip plugin code here


            var container = $(this);
            var table = chart.head(settings);


            if (settings.data.length > 0) {
                var body = chart.body(settings);
                table.append(body);
            } else {
                var tr = $("<tr>");
                var td = $("<td>", {
                    "colspan": settings.leftth.length + (((settings.hourinterval * 60) / settings.mininterval) * settings.hours.length),
                    "align": "center",
                    "style": "font-size:100%;font-weight:bold;color:#808080;",
                    "text": "--- No data found ---"
                });


                tr.append(td);
                table.append(tr);

            }


            container.html("");
            container.append(table);

        });

    };

    var chart = {

        head: function (settings) {

            var table = $("<table>", {
                "width": settings.containerpx,
                "cellspacing": 0,
                "cellpadding": 0,
                "border": 0,
                "class": "timeview"
            });

            var thead = $("<thead>");


            var menu_tr = $("<tr>");
            menu_tr.append($("<th>", {
                "class": "menu_bar",
                "text": settings.title,
                "colspan": settings.leftth.length + (((settings.hourinterval * 60) / settings.mininterval) * settings.hours.length)
            }));

            var tr = $("<tr>");



            for (var i = 0; i < settings.leftth.length; i++) {
                tr.append($("<th>", {
                    "width": settings.leftth[i].width,
                    "align": settings.leftth[i].align,
                    "text": settings.leftth[i].text
                }));
            }


            for (var i = 0; i < settings.hours.length; i++) {
                var th = $("<th>", {
                    "width": settings.hourintervalpx,
                    "colspan": settings.hourintervalcolspan,
                    "align": "center"
                });

                th.html("<div style=\"width:" + settings.hourintervalpx + "px" + "\">" + settings.hours[i].start + "<div style=\"heigth:1px;\"></div>" + "-" + "<div style=\"heigth:1px;\"></div>" + settings.hours[i].end + "</div>");
                tr.append(th);
            }

            thead.append(menu_tr);
            thead.append(tr);
            table.append(thead);

            return table;

        },
        body: function (settings) {



            var arr;
            var trarr = [];
            var lefttd = this.lefttd(settings);
            var righttd = this.righttd(settings);
            var rowcount = 1;
            var temp_lefttd;
            var temp_righttd;
            var isaddedtr;


            var shift = "shift";
            var booking = "booking";
            var leave = "leave";



            for (var i = 0; i < settings.data.length; i++) {

                isaddedtr = 0;

                arr = settings.data[i];

                if (arr.hasOwnProperty(shift)) {

                    temp_lefttd = lefttd.slice();
                    temp_righttd = righttd.slice();

                    this.plotleftcell(settings, arr, temp_lefttd, shift, rowcount);


                    this.plotrightcell(settings, arr[shift], temp_righttd, shift);


                    trarr.push("<tr>" + temp_lefttd.join("") + temp_righttd.join("") + "</tr>");
                    isaddedtr++;
                }

                if (arr.hasOwnProperty(booking)) {


                    temp_lefttd = lefttd.slice();
                    temp_righttd = righttd.slice();

                    this.plotleftcell(settings, arr, temp_lefttd, booking, 0);



                    var cloneArr = new Array();
                    for (var c = 0; c < temp_lefttd.length; c++) {

                        var rowspan = settings.leftth[c].rowspan;
                        if (!rowspan && rowspan <= 0) {
                            cloneArr.push(temp_lefttd[c]);

                        }
                    }

                    temp_lefttd = cloneArr;



                    this.plotrightcell(settings, arr[booking], temp_righttd, booking);

                    trarr.push("<tr>" + temp_lefttd.join("") + temp_righttd.join("") + "</tr>");

                }


                if (arr.hasOwnProperty(leave)) {


                    temp_lefttd = lefttd.slice();
                    temp_righttd = righttd.slice();

                    this.plotleftcell(settings, arr, temp_lefttd, leave, 0);


                    var cloneArr = new Array();
                    for (var c = 0; c < temp_lefttd.length; c++) {

                        var rowspan = settings.leftth[c].rowspan;
                        if (!rowspan && rowspan <= 0) {
                            cloneArr.push(temp_lefttd[c]);

                        }
                    }

                    temp_lefttd = cloneArr;

                    this.plotrightcell(settings, arr[leave], temp_righttd, leave);

                    trarr.push("<tr>" + temp_lefttd.join("") + temp_righttd.join("") + "</tr>");

                }

                if (isaddedtr)
                    rowcount++;
            }




            var tbody = $("<tbody>");
            $(tbody).append(trarr.join(''));

            return tbody;
        },

        lefttd: function (settings) {

            var lefttdarr = [];
            var align;
            var td;
            var rowspan;

            for (var i = 0; i < settings.leftth.length; i++) {

                align = "left";

                if (i == 0)
                    align = "center";

                rowspan = settings.leftth[i].rowspan;

                if (rowspan && rowspan > 0) {
                    rowspan = "rowspan=\"" + rowspan + "\"";
                } else {
                    rowspan = ""
                }

                lefttdarr[i] = "<td " + rowspan + " align=\"" + align + "\" width=\"" + settings.leftth[i].width + "px" + "\" class=\"cursor\"></td>";
            }

            return lefttdarr;
        },

        righttd: function (settings) {

            var righttdarr = [];
            var align;
            var td;

            for (var h = 0; h < settings.hours.length; h++) {
                for (var m = 0; m < settings.mincells; m++) {
                    righttdarr.push("<td width=\"" + settings.minintervalpx + "px" + "\" align=\"left\" valign=\"top\" class=\"interval\"></td>");
                }
            }

            return righttdarr;
        },

        plotleftcell: function (settings, dataarr, cellsarr, code, rowcount) {



            for (var c = 0; c < cellsarr.length; c++) {



                var index = settings.leftth[c].index;
                var val = dataarr[index];

                if (rowcount == 0) {
                    val = "";
                }

                if (index.toLowerCase() == "no") {
                    if (rowcount > 0) {
                        val = rowcount + ".";
                    } else {
                        val = "";
                    }
                }

                if (index.toLowerCase() == "code") {

                    //val = dataarr[code + "code"];
                    val = code.replace(/\b[a-z]/g, function ($0) {
                        return $0.toUpperCase();
                    });

                }


                if (val != undefined) {
                    var width = settings.leftth[c].width;
                    var bar = "<div class=\"lslot\" style=\"width:" + width + "px;\">" +
                                    val +
                               "</div>";
                    cellsarr[c] = this.appendtext(cellsarr[c], bar, val);
                }
            }

        },

        plotrightcell: function (settings, dataarr, cellsarr, block) {

            var start_date;
            var end_date;
            var start_hour;
            var start_min;
            var end_hour;
            var end_min;
            var index;
            var needle;


            for (var c = 0; c < dataarr.length; c++) {

                start_date = new Date(Date.parse(dataarr[c]["start"]));
                end_date = new Date(Date.parse(dataarr[c]["end"]));
                start_hour = start_date.getHours();
                start_min = start_date.getMinutes();
                end_hour = end_date.getHours();
                end_min = end_date.getMinutes();

                /**
                if (end_date.compareTo(start_date) == 0 || end_date.compareTo(start_date) == -1) {
                continue;
                }**/

                needle = start_hour;

                index = (this.findhourslot(settings, needle));
                if (index == -1) {
                    continue;
                }

                if (start_hour == 0 && end_hour == 0)
                    start_hour = 24;

                if (end_hour == 0)
                    end_hour = 24;



                var cellsforhourinterval = settings.hourintervalcolspan; //number of cells for each hour interval
                var cellplotpos = (index * cellsforhourinterval) + Math.floor(start_min / settings.mininterval); // starting position for bar
                var cellplotlengthinmin = ((end_hour - start_hour) * 60) + (end_min - start_min); // total number of mins for length of bar

                var width = Math.round(((cellplotlengthinmin / settings.mininterval) * settings.actualmintervalpx)); // calculate pixel needed for bar
                //width += Math.floor((cellplotlengthinmin / settings.mininterval / cellsforhourinterval)) * settings.cellpaddingleftright; //add cellpaddingleftright if cellsforhourinterval is fully occuppied for each hour interval

                if (dataarr[c]["localusage"] == "Malaysia") {
                    var bar = "<div class=\"slot\" style=\"width:" + width + "px;background-color:" + dataarr[c]["color"] + ";\">" +
                        "<a style=\"" + (dataarr[c]["link"] != " " ? "text-decoration: underline;" : "text-decoration: none") + "\" href=\"" + (dataarr[c]["link"] != " " ? dataarr[c]["link"] : "#") + "\" title=\"" + dataarr[c]["title"] + "\" target=\"_blank\" " + (dataarr[c]["link"] != " " ? " " : "onclick=\"return false;\"") + ">" +
                        dataarr[c]["text"] + "<img style=\"width:26px;\" src=\"/images/Malaysia-Icon.png\" />" +
                        "</a>" +
                        "</div>";
                } else {
                    var bar = "<div class=\"slot\" style=\"width:" + width + "px;background-color:" + dataarr[c]["color"] + ";\">" +
                        "<a style=\"" + (dataarr[c]["link"] != " " ? "text-decoration: underline;" : "text-decoration: none") + "\" href=\"" + (dataarr[c]["link"] != " " ? dataarr[c]["link"] : "#") + "\" title=\"" + dataarr[c]["title"] + "\" target=\"_blank\" " + (dataarr[c]["link"] != " " ? " " : "onclick=\"return false;\"") + ">" +
                        dataarr[c]["text"] +
                        "</a>" +
                        "</div>";
                }
                cellsarr[cellplotpos] = this.appendtext(cellsarr[cellplotpos], bar, dataarr[c]["text"]);

            }


        },

        appendtext: function (context, val, text) {
            var obj = $(context).append(val).attr("title", text);
            return $("<div>").append(obj.clone()).html();
        },

        findhourslot: function (settings, needle) {

            var index = -1;

            if (needle < 10) {
                needle = "0" + needle;
            }

            //needle += "00";

            for (var i = 0; i < settings.hours.length; i++) {
                if (settings.hours[i]["start"] == needle) {
                    index = i;
                    break;
                }
            }

            return index;
        }
    };


})(jQuery);