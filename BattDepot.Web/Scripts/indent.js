var notificator = $.connection.notificationHub,
    $indentNotify = $('#indent-trigger'),
    $indentNotifyCounter = $indentNotify.find('.count'),
    indentCount = 0,
    $indentFlyer = $('#myMail'),
    $indentFlyerContent = $indentFlyer.find('.modal-body table'),
    ///
    $confirmDocNotify = $('#ConfirmDocument-trigger'),
    $confirmDocNotifyCounter = $confirmDocNotify.find('.count'),
    confirmDocCount = 0,
    $confirmDocFlyer = $('#MyLC'),
    $confirmDocFlyerContent = $confirmDocFlyer.find('.modal-body table'),
    ///
    documentTitle = document.title;

notificator.client.notify = function(dataJson, dataType) {
    try {
        var data = eval('(' + dataJson + ')');
        new window[dataType](data).notify();
    } catch(e) {
    }
};

///////////////////////////////////////////////////////////////////////////
/// Indent notification
///////////////////////////////////////////////////////////////////////////

var Indent = (function() {

    function Indent(data) {
        this.indent = data;
    }

    Indent.prototype.notify = function() {
        if (this.indent.Status == 0) { // initial
            this.notifyInitial();
            $.sticky('Indent <a href="/Indents/Details/' + this.indent.Id + "\">" + this.indent.IndentNo + '</a> has been created', { type: 'st-success' });
        } else if (this.indent.Status == 1) { // denied
            $.sticky('Indent <a href="/Indents/Details/' + this.indent.Id + "\">" + this.indent.IndentNo + '</a> has been denied', { type: 'st-error' });
        }

        ++indentCount;
        $indentNotifyCounter.html(indentCount);
        $indentNotify.addClass('new-effect');

        document.title = '(' + indentCount + ') ' + documentTitle;
    };

    Indent.prototype.notifyInitial = function() {
        var indentCifSing = 0;
        for (var i = 0; i < this.indent.IndentLines.length; i++) {
            indentCifSing += this.indent.IndentLines[i].Cif * this.indent.IndentLines[i].Quantity * this.indent.ExchangeRatio;
        }

        var row = '\
        	<tr class="success">\
    			<td><a href="/Indents/Details/' + this.indent.Id + "\">" + this.indent.IndentNo + '</a></td>\
    			<td>' + this.indent.SupplierName + '</td>\
    			<td>' + accounting.formatNumber(indentCifSing, 2) + " " + this.indent.ExchangeBaseName + '</td>\
    			<td>' + moment(this.indent.Date).format('MM/DD/YYYY') + '</td>\
    		</tr>';

        $indentFlyerContent.find('tbody').append(row);
    };

    return Indent;

})();

$indentNotify.on('click', function(e) {
    e.preventDefault();

    $indentNotify.removeClass('new-effect');
    document.title = documentTitle;
    indentCount = 0;
});

/////////////////////////////////////////////////////////////////////////////
/// Consolidate notification
/////////////////////////////////////////////////////////////////////////////

//consolidateNotificator.client.notify = function(consolidateString) {
//    var consolidate = eval("(" + consolidateString + ")");

//    if (consolidate.StatusRecord.Status == 0) { // initial
//        $.sticky("consolidate <a href=\"/Indents/Details/" + consolidate.Id + "\">" + consolidate.IndentNo + "</a> has been created", { type: 'st-success' });
//    } else if (consolidate.StatusRecord.Status == 1) { // denied
//        $.sticky("consolidate <a href=\"/Indents/Details/" + consolidate.Id + "\">" + consolidate.IndentNo + "</a> has been denied", { type: 'st-error' });
//    }

//    ++indentCount;
//    $indentNotifyCounter.html(indentCount);
//    $indentNotify.addClass("new-effect");

//    document.title = '(' + indentCount + ') ' + documentTitle;
//};

////////////////////////////////////////////////////////////////////////////
/// ConfirmDocument notification
////////////////////////////////////////////////////////////////////////////

var ConfirmDocument = (function() {

    function ConfirmDocument(data) {
        this.confirmDoc = data;
    }

    ConfirmDocument.prototype.notify = function() {
        if (this.confirmDoc.Status == 0) { // initial
            this.notifyInitial();
            $.sticky('Indent <a href="/ConfirmDocuments/Details/' + this.confirmDoc.Id + '">#' + this.confirmDoc.Id + '</a> has been created', { type: 'st-success' });
        } else if (this.confirmDoc.Status == 1) { // denied
            $.sticky('Indent <a href="/ConfirmDocuments/Details/' + this.confirmDoc.Id + '">#' + this.confirmDoc.IndentNo + '</a> has been denied', { type: 'st-error' });
        }

        ++confirmDocCount;
        $confirmDocNotifyCounter.html(confirmDocCount);
        $confirmDocNotify.addClass('new-effect');

        document.title = '(' + confirmDocCount + ') ' + documentTitle;
    };

    ConfirmDocument.prototype.notifyInitial = function() {
        var row = '\
        	<tr class="success">\
    			<td><a href="/ConfirmDocuments/Details/' + this.confirmDoc.Id + '">#' + this.confirmDoc.Id + '</a></td>\
                <td>';

        for (var i = 0; i < this.confirmDoc.Indent.length; i++)
            row += this.confirmDoc.Indent[0].IndentNo;

        row += '\
                </td>\
            </tr>';

        $confirmDocFlyerContent.find('tbody').append(row);
    };

    return ConfirmDocument;
})();

$confirmDocNotify.on('click', function(e) {
    e.preventDefault();

    $confirmDocNotify.removeClass('new-effect');
    document.title = documentTitle;
    confirmDocCount = 0;
});



////////////////////////////////////////////////////////////////////////////
/// IndentCtrl for AngularJS
////////////////////////////////////////////////////////////////////////////

//function IndentCtrl ($scope) {
//	$scope.lines = [];

//	$scope.add = function () {
//		$scope.lines.push({ $scope.modelCode, $scope.cif, $scope.modelColor, $scope.year, $scope.quantity });
//		$scope.modelCode = '';
//		$scope.cif = '';
//		$scope.modelColor = '';
//		$scope.year = '';
//		$scope.quantity = '';
//	}
//}
