var D=new function(){var t=this,f='ShockwaveFlash',s=f+'.'+f,w=function(o){try{t.v=o.GetVariable("$version")}catch(e){}},r=[[s+'.7',w],[s+'.6',function(o){t.v='6,0,21'
try{o.AllowScriptAccess='always';w(o)}catch(e){}}],[s,w]],n=navigator,p=n.plugins,e='enabledPlugin',d='description',i=0
t.i=0;t.v=-1
t.D=function(){if(p&&p.length>0){var a='application/x-shockwave-flash',m=n.mimeTypes
if(m&&m[a]&&m[a][e]&&m[a][e][d]){t.v=m[a][e][d];t.i=1}}else
if(n.appVersion.indexOf('Mac')==-1&&window.execScript){for(;i<r.length&&t.v==-1;i++){try{r[i][1](new ActiveXObject(r[i][0]));t.i=1}catch(e){}}}}()};