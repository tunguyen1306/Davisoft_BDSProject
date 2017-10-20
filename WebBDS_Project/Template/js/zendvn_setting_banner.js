jQuery(document).ready(function($){
	var default_top = 0;	
	function settingBanner(){
		var bodyWidth 	= $("body").width();
		var main = $(".container div:first-child");
		var width 		= main.width();
		var pos 		= main.position();
		
		var leftElement = $("#zendvn-real-estate-bn-l");
		var leftWidth 	= leftElement.width();
		if(leftWidth == 0) leftWidth = 100;
		
		var left 		= (parseInt(bodyWidth) - parseInt(width))/2 - parseInt(leftWidth) - 18;
		
		var padTop = $(".container").css('padding-top');
		default_top		= parseInt(pos.top) - parseInt(padTop) + 4;
		
		leftElement.css({
	        disable: "block",
	        position: "fixed",
	        top: default_top + "px",
	        left: left + "px"
	    }).slideDown("slow");
		$("#zendvn-real-estate-bn-r").css({
			disable: "block",
	        position: "fixed",
	        top: default_top + "px",
	        right: left + "px"
	    }).slideDown("slow");
	}
	$(window).load(function(){
		settingBanner();
		set = true;
	});
	$(window).on( "resize",function() {
		settingBanner();
		set = true;
	});
	var timeoutId = null;
	$(window).on( "scroll",function() {
		if(timeoutId){
			clearTimeout(timeoutId );  
		}
		timeoutId = setTimeout(function(){
			var top = $(this).scrollTop();
			if(top >= 100){
				var pos = $("body").position();
				$("#zendvn-real-estate-bn-r").animate({
			        top: pos.top + "px",
			    });
				$("#zendvn-real-estate-bn-l").animate({
			        top: pos.top + "px",
			    });
			}else{
				$("#zendvn-real-estate-bn-r").animate({
			        top: default_top + "px",
			    });
				$("#zendvn-real-estate-bn-l").animate({
			        top: default_top + "px",
			    });
			}
		}, 100);
	});
});