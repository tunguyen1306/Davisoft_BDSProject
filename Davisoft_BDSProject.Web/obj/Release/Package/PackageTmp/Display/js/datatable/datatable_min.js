  $.fn.dataTable.Api.register( 'column().data().sum()', function () {
    return this.reduce( function (a, b) {
        var x = parseFloat( a ) || 0;
        var y = parseFloat( b ) || 0;
        return x + y;
    } );
} );
 
/* Init the table and fire off a call to get the hidden nodes. */
$(document).ready(function() {
    var table = $('#example').DataTable();
     
    $('<button>Click to sum age in all rows</button>')
        .prependTo( '#demo' )
        .on( 'click', function () {
            alert( 'Column sum is: '+ table.column( 3 ).data().sum() );
        } );
 
    $('<button>Click to sum age of visible rows</button>')
        .prependTo( '#demo' )
        .on( 'click', function () {
            alert( 'Column sum is: '+ table.column( 3, {page:'current'} ).data().sum() );
        } );
} );