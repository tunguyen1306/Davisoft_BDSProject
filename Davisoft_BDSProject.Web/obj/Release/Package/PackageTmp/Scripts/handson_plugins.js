define([
    'jquery',
    'underscore',
    'app/app',
    'jquery.handsontable',
    'moment',

    'jquery.tools'
], function($, _, app, Handsontable, moment) {

    function _validateCell(instance, row, prop, value) {
        var col = instance.propToCol(prop);
        var validator = instance.getCellMeta(row, col).validator;
        return validator? validator(instance, row, prop, value) : true;
    }

    return {

        /**
         * Validators that can be set against a column under the 'validator'
         * property, e.g.:
         * foo.handsontable({
         *      ...
         *      columns: [{
         *          data: "name",
         *          validator: TablePlugins.validators.requiredText
         *      }]
         * }
         */
        validators: {

            requiredText: function(instance, row, prop, value) {
                return Boolean(value);
            },

            uniqueText: function(instance, row, prop, value) {
                for(var i = 0; i < (instance.countRows() - instance.getSettings().minSpareRows); ++i) {
                    var otherValue = instance.getDataAtCell(i, instance.propToCol(prop));
                    if(i != row && value == otherValue) {
                        return false;
                    }
                }
                return true;
            },

            requiredUniqueText: function(instance, row, prop, value) {
                if(!Boolean(value)) {
                    return false;
                }
                for(var i = 0; i < (instance.countRows() - instance.getSettings().minSpareRows); ++i) {
                    var otherValue = instance.getDataAtCell(i, instance.propToCol(prop));
                    if(i != row && value == otherValue) {
                        return false;
                    }
                }
                return true;
            },

            number: function(instance, row, prop, value) {
                return _.isNull(value) || _.isFinite(value);
            },

            requiredNumber: function(instance, row, prop, value) {
                return _.isFinite(value);
            },

            percentage: function(instance, row, prop, value) {
                return _.isNull(value) || (_.isFinite(value) && value >= 0 && value <= 100);
            },

            requiredPercentage: function(instance, row, prop, value) {
                return _.isFinite(value) && value >= 0 && value <= 100;
            },

            date: function(instance, row, prop, value) {
                return _.isNull(value) || _.isDate(value);
            },

            requiredDate: function(instance, row, prop, value) {
                return _.isDate(value);
            },

            // A list that will be managed as a ``DetailsListCell``.
            // The column definition must have a property ``detailsColumns``
            // that is a list like ``columns``, describing the columns of
            // the child list.
            detailsList: function(instance, row, prop, value) {
                var col = instance.propToCol(prop),
                    cellMeta = instance.getCellMeta(row, col),
                    detailsColumns = cellMeta.detailsColumns;

                if(!detailsColumns) {
                    console.error("Expected to find detailsColumns property for " + prop);
                    return true;
                }

                if(_.isNull(value))
                    return true;

                var data = {};
                function validateCell(col) {
                    var validator = col.validator;
                    // XXX: Almost, but not quite; instance is not available, so some validators won't work
                    return validator? validator(null, data, col.data, data[col.data]) : true;
                }

                for(var i = 0; i < value.length; ++i) {
                    data = value[i];
                    if(!_.every(detailsColumns, validateCell))
                        return false;
                }

                return true;
            }

        },

        /**
         * Validate a single cell, returning true or false
         */
        validateCell: _validateCell,

        /**
         * Validate a change in a way that is suitable as an onBeforeChange
         * handler, rejecting changes that would result in an invalid cell
         */
        validateBeforeChange: function(data, source) {
            var instance = $(this).data('handsontable');

            _.each(data, function(change) {
                var row      = change[0],
                    prop     = change[1],
                    oldValue = change[2],
                    newValue = change[3];

                if(!_validateCell(instance, row, prop, newValue)) {
                    change[3] = oldValue;
                }
            });
        },

        /**
         * Validate an entire table. Returns a promise. Success callbacks are
         * called with the table instance as an argument. Failure callbacks are
         * called with the instance and a list of [row, prop] pairs where there
         * were validation errors.
         */
        validateTable: function(instance) {
            var dfd        = $.Deferred(),
                invalid    = [],
                data       = instance.getData(),
                spareRows  = instance.getSettings().minSpareRows,
                dataLength = data.length - spareRows;

            _.each(data, function(element, index) {
                if(index >= dataLength) return;
                _.each(element, function(value, key) {
                    if(!_validateCell(instance, index, key, value)) {
                        invalid.push([index, key]);
                    }
                });
            });

            if(invalid.length > 0) {
                dfd.reject(instance, invalid);
            } else {
                dfd.resolve(instance);
            }

            return dfd.promise();
        },

        /**
         * Cell types with inline validation
         */

        // Plain text or a number
        TextCell: {
            renderer: function(instance, td, row, col, prop, value, cellProperties) {
                Handsontable.TextCell.renderer.apply(this, arguments);
                if(row < (instance.countRows() - instance.getSettings().minSpareRows)) {
                    td.className = _validateCell(instance, row, prop, value)? '': 'error';
                }
            },
            editor: Handsontable.TextEditor
        },

        // A date, using a date picker
        DateCell: {
            renderer: function(instance, td, row, col, prop, value, cellProperties) {
                Handsontable.TextCell.renderer.apply(this, arguments);
                if(row < (instance.countRows() - instance.getSettings().minSpareRows)) {
                    td.className = _validateCell(instance, row, prop, value)? '': 'error';
                    if(value) {
                        var dateValue = moment(value);
                        td.innerHTML = dateValue.format(app.momentDateFormat);
                    }
                }
            },
            editor: function (instance, td, row, col, prop, keyboardProxy, cellProperties) {
                if(_.isUndefined(cellProperties))
                    cellProperties = {};

                $(td).dateinput({
                    format: app.datePickerDateFormat,
                    firstDay: 1,
                    onShow: function() {
                        var tdPosition = $(td).offset();
                        $("#calroot").offset(tdPosition);
                    },
                    change: function() {
                        instance.setDataAtCell(row, prop, this.getValue());
                    }
                });

                keyboardProxy.on("keydown.editor", function(event) {
                     //catch CTRL but not right ALT (which in some systems triggers ALT+CTRL)
                    var ctrlDown = (event.ctrlKey || event.metaKey) && !event.altKey;
                    if(!ctrlDown && Handsontable.helper.isPrintableChar(event.keyCode)) {
                        $(td).data('dateinput').show();
                        event.stopPropagation();
                    }
                });

                instance.view.wt.update('onCellDblClick', function() {
                    $(td).data('dateinput').show();
                });

                return function() {
                    keyboardProxy.off(".editor");
                    instance.view.wt.update('onCellDblClick', null);
                };
            }
        },

        // A currency value, stored as a number, but rendered with a £ prefix
        CurrencyCell: {
            renderer: function(instance, td, row, col, prop, value, cellProperties) {
                Handsontable.TextCell.renderer.apply(this, arguments);
                if(!_.isNull(value) && value !== "") {
                    td.innerHTML = "£" + td.innerHTML;
                }
                if(row < (instance.countRows() - instance.getSettings().minSpareRows)) {
                    td.className = _validateCell(instance, row, prop, value)? '': 'error';
                }
            },
            editor: Handsontable.TextEditor
        },

        // A percentage, stored as a number, but rendered with a % suffix
        PercentageCell: {
            renderer: function(instance, td, row, col, prop, value, cellProperties) {
                Handsontable.TextCell.renderer.apply(this, arguments);
                if(!_.isNull(value) && value !== "") {
                    td.innerHTML = td.innerHTML + "%";
                }
                if(row < (instance.countRows() - instance.getSettings().minSpareRows)) {
                    td.className = _validateCell(instance, row, prop, value)? '': 'error';
                }
            },
            editor: Handsontable.TextEditor
        },

        // Autocomplete with validation
        AutocompleteCell: {
            renderer: function(instance, td, row, col, prop, value, cellProperties) {
                Handsontable.AutocompleteCell.renderer.apply(this, arguments);
                if(row < (instance.countRows() - instance.getSettings().minSpareRows)) {
                    td.className = _validateCell(instance, row, prop, value)? '': 'error';
                }
            },
            editor: Handsontable.AutocompleteEditor
        },

        // A list of values. Renders the number of items in the list. On edit,
        // calls a callback function with the list, which can be edited inplace.
        // For example:
        //
        //  columns: [
        //    {
        //      data: "allocations",
        //      type: TablePlugins.ChildListCell,
        //      showEditor: function(instance, td, row, col, prop, cellProperties, value) {
        //          ...
        //      }
        //    },
        //    ...
        // ]
        //
        // If the current value is null, a new list will be created
        DetailsListCell: {
            renderer: function(instance, td, row, col, prop, value, cellProperties) {
                td.innerHTML = value? value.length + "..." : "0...";
                if(row < (instance.countRows() - instance.getSettings().minSpareRows)) {
                    td.className = _validateCell(instance, row, prop, value)? '': 'error';
                }
            },
            editor: function (instance, td, row, col, prop, keyboardProxy, cellProperties) {
                if(_.isUndefined(cellProperties))
                    cellProperties = {};

                var value = instance.getDataAtCell(row, col);
                if(_.isNull(value)) { // turn null into a list
                    var rowData = instance.getData()[row];
                    value = rowData[prop] = [];
                }

                keyboardProxy.on("keydown.editor", function(event) {
                     //catch CTRL but not right ALT (which in some systems triggers ALT+CTRL)
                    var ctrlDown = (event.ctrlKey || event.metaKey) && !event.altKey;
                    if(!ctrlDown && Handsontable.helper.isPrintableChar(event.keyCode)) {
                        cellProperties.showEditor(instance, td, row, col, prop, cellProperties, value);
                        event.stopPropagation();
                    }
                });

                instance.view.wt.update('onCellDblClick', function() {
                    cellProperties.showEditor(instance, td, row, col, prop, cellProperties, value);
                });

                return function() {
                    keyboardProxy.off(".editor");
                    instance.view.wt.update('onCellDblClick', null);
                };
            }
        }

    };

});
