
/*
 *
 * More info at [www.dropzonejs.com](http://www.dropzonejs.com)
 *
 * Copyright (c) 2012, Matias Meno
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 *
 */

(function () {

    var content =
        "<div class=\"dz-preview dz-file-preview\">\n" +
        "<div class=\"tools final \" >" +
        "<div onclick='Edit(this)'  title=\"Edit\" class=\"btn cusIconImg cusbtn cbtn-edit hidden\"><i class=\"glyphicon glyphicon-pencil\"></i></div>" +
        "<div onclick='DeleteUpload(this)' title=\"Delete\" class=\"btn cusIconImg cusbtn cusbtn-del\">" +
    "<svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" width=\"12px\" height=\"12px\" viewBox=\"0 0 15.809 20\" enable-background=\"new 0 0 15.809 20\" xml:space=\"preserve\"><g>" +
    "<path fill=\"#FFFFFF\" d=\"M14.444,2.354h-2.67V0.62c0-0.35-0.281-0.62-0.631-0.62c-0.034,0-0.057,0.012-0.067,0.022" +
    "C11.064,0.012,11.042,0,11.03,0H4.664c-0.349,0-0.62,0.27-0.62,0.62v1.735H1.364C0.597,2.354,0,2.952,0,3.718V5.95h1.183v12.698" +
    "C1.183,19.414,1.769,20,2.535,20h10.738c0.767,0,1.362-0.586,1.362-1.353V5.95h1.173V3.718C15.809,2.952,15.211,2.354,14.444,2.354" +
    "L14.444,2.354z M5.284,1.239h5.239v1.115H5.284V1.239z M13.386,18.647c0,0.079-0.034,0.113-0.112,0.113H2.535" +
    "c-0.079,0-0.113-0.034-0.113-0.113V5.95h10.963V18.647z M14.558,4.71H1.251V3.718c0-0.079,0.033-0.125,0.113-0.125h13.081" +
    "c0.079,0,0.113,0.046,0.113,0.125V4.71z M14.558,4.71\"/>" +
    "<rect x=\"10.175\" y=\"7.402\" fill=\"#FFFFFF\" width=\"1.25\" height=\"10.254\"/>" +
    "<rect x=\"7.29\" y=\"7.402\" fill=\"#FFFFFF\" width=\"1.251\" height=\"10.254\"/>" +
"<rect x=\"4.406\" y=\"7.402\" fill=\"#FFFFFF\" width=\"1.25\" height=\"10.254\"/></g></svg></div></div>" +
        "<div class=\"tools hidden\" > " +
        "<div class=\"btn-group docs-buttons\">" +

          "<button title=\"Zoom In\" data-option=\"-0.1\" data-method=\"zoom\" class=\"cusIconImg btn cusbtn\" type=\"button\">" +
            "<span title=\"\" data-toggle=\"tooltip\" class=\"docs-tooltip\" data-original-title=\"$().cropper(&quot;zoom&quot;, -0.1)\">" +
              "<svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\"width=\"12px\" height=\"12px\" viewBox=\"0 0 20 20.637\" enable-background=\"new 0 0 20 20.637\" xml:space=\"preserve\"><g><g>" +
              "<path fill=\"#666766\" d=\"M19.186,20.608c-0.213,0-0.418-0.088-0.576-0.246l-5.834-5.834c-1.385,1.012-3.021,1.546-4.739,1.546    C3.605,16.074,0,12.469,0,8.037S3.605,0,8.037,0c4.431,0,8.037,3.605,8.037,8.037c0,1.986-0.736,3.894-2.077,5.381l5.768,5.778    c0.314,0.329,0.314,0.851,0,1.166C19.624,20.515,19.407,20.608,19.186,20.608z M8.037,1.647c-3.524,0-6.391,2.867-6.391,6.39    s2.867,6.391,6.391,6.391s6.39-2.867,6.39-6.391S11.561,1.647,8.037,1.647z\"/>" +
		"<path fill=\"#666766\" d=\"M8.037,15.969c1.783,0,3.421-0.586,4.75-1.579l5.897,5.897c0.144,0.144,0.323,0.216,0.502,0.216    c0.18,0,0.371-0.072,0.503-0.216c0.275-0.275,0.275-0.729,0-1.017l-5.838-5.85c1.304-1.412,2.117-3.302,2.117-5.384    c0-4.366-3.553-7.931-7.931-7.931S0.105,3.671,0.105,8.037S3.659,15.969,8.037,15.969L8.037,15.969z M8.037,1.542    c3.577,0,6.496,2.907,6.496,6.496s-2.906,6.496-6.496,6.496c-3.577,0-6.496-2.907-6.496-6.496S4.448,1.542,8.037,1.542    L8.037,1.542z M8.037,1.542\"/>" +
		"<rect fill=\"none\" width=\"19.997\" height=\"20.637\"/></g>" +
	"<path fill=\"#666766\" d=\"M7.286,8.836H5.118c-0.454,0-0.823-0.369-0.823-0.823c0-0.454,0.369-0.824,0.823-0.824h2.167h1.646h2.167   c0.454,0,0.824,0.37,0.824,0.824c0,0.454-0.37,0.823-0.824,0.823H8.932H7.286z\"/>" +
	"<path fill=\"#666766\" d=\"M5.118,8.731h2.273h1.436h2.272c0.396,0,0.718-0.323,0.718-0.717c0-0.395-0.322-0.718-0.718-0.718H8.827   H7.391H5.118C4.723,7.295,4.4,7.619,4.4,8.014C4.4,8.408,4.723,8.731,5.118,8.731L5.118,8.731z M5.118,8.731\"/>" +
	"</g>" +
"</svg></span> </button>" +
          "<button title=\"Zoom Out\" data-option=\"0.1\" data-method=\"zoom\" class=\"btn cusIconImg cusbtn\" type=\"button\">" +
            "<span title=\"\" data-toggle=\"tooltip\" class=\"docs-tooltip\" data-original-title=\"$().cropper(&quot;zoom&quot;, 0.1)\">" +
             "<svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" width=\"12px\" height=\"12px\" viewBox=\"0 0 20 20.637\" enable-background=\"new 0 0 20 20.637\" xml:space=\"preserve\"><g><g>\"+" +
		"<path fill=\"#666766\" d=\"M19.186,20.608c-0.213,0-0.418-0.088-0.576-0.246l-5.834-5.834c-1.385,1.012-3.021,1.546-4.739,1.546    C3.605,16.074,0,12.469,0,8.037S3.605,0,8.037,0c4.431,0,8.037,3.605,8.037,8.037c0,1.986-0.736,3.894-2.077,5.381l5.768,5.778    c0.314,0.329,0.314,0.851,0,1.166C19.624,20.515,19.407,20.608,19.186,20.608z M8.037,1.647c-3.524,0-6.391,2.867-6.391,6.39    s2.867,6.391,6.391,6.391s6.39-2.867,6.39-6.391S11.561,1.647,8.037,1.647z\"/>" +
		"<path fill=\"#666766\" d=\"M8.037,15.969c1.783,0,3.421-0.586,4.75-1.579l5.897,5.897c0.144,0.144,0.323,0.216,0.502,0.216    c0.18,0,0.371-0.072,0.503-0.216c0.275-0.275,0.275-0.729,0-1.017l-5.838-5.85c1.304-1.412,2.117-3.302,2.117-5.384    c0-4.366-3.553-7.931-7.931-7.931S0.105,3.671,0.105,8.037S3.659,15.969,8.037,15.969L8.037,15.969z M8.037,1.542    c3.577,0,6.496,2.907,6.496,6.496s-2.906,6.496-6.496,6.496c-3.577,0-6.496-2.907-6.496-6.496S4.448,1.542,8.037,1.542    L8.037,1.542z M8.037,1.542\"/>" +
		"<rect fill=\"none\" width=\"19.997\" height=\"20.637\"/>	</g>" +
	"<path fill=\"#666766\" d=\"M8.109,11.827c-0.454,0-0.823-0.369-0.823-0.823V8.836H5.118c-0.454,0-0.823-0.369-0.823-0.823   c0-0.454,0.369-0.824,0.823-0.824h2.167V5.022c0-0.454,0.369-0.823,0.823-0.823s0.823,0.369,0.823,0.823V7.19h2.167   c0.454,0,0.824,0.37,0.824,0.824c0,0.454-0.37,0.823-0.824,0.823H8.932v2.167C8.932,11.458,8.563,11.827,8.109,11.827z\"/>" +
	"<path fill=\"#666766\" d=\"M5.118,8.731h2.273v2.273c0,0.395,0.323,0.718,0.718,0.718c0.395,0,0.718-0.323,0.718-0.718V8.731h2.272   c0.396,0,0.718-0.323,0.718-0.717c0-0.395-0.322-0.718-0.718-0.718H8.827V5.022c0-0.395-0.323-0.718-0.718-0.718   c-0.395,0-0.718,0.323-0.718,0.718v2.273H5.118C4.723,7.295,4.4,7.619,4.4,8.014C4.4,8.408,4.723,8.731,5.118,8.731L5.118,8.731z    M5.118,8.731\"/>" +
"</g>" +
"</svg></span> </button>" +

         "<button title=\"Move\" data-option=\"move\" data-method=\"setDragMode\" class=\"btn cusIconImg\" type=\"button\">" +
        " <span title=\"\" data-toggle=\"tooltip\" class=\"docs-tooltip\" data-original-title=\"$().cropper(&quot;setDragMode&quot;, &quot;move&quot;)\">" +
        " <svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\"width=\"12px\" height=\"12px\" viewBox=\"0 0 12 12\" enable-background=\"new 0 0 12 12\" xml:space=\"preserve\"><g>" +
	"<path fill=\"#666766\" d=\"M11.969,5.848l-0.086-0.129l-1.602-1.602c-0.152-0.156-0.41-0.156-0.563,0c-0.156,0.156-0.156,0.41,0,0.567l0.914,0.918H6.398V1.367l0.918,0.914c0.156,0.157,0.41,0.157,0.566,0c0.156-0.156,0.156-0.41,0-0.562L6.277,0.113L6.152,0.031L6,0" +
    "L5.848,0.031L5.723,0.113L4.117,1.719c-0.156,0.152-0.156,0.406,0,0.562c0.156,0.157,0.41,0.157,0.567,0l0.918-0.914v4.235H1.367" +
    "l0.914-0.918c0.157-0.157,0.157-0.411,0-0.567c-0.156-0.156-0.41-0.156-0.562,0L0.113,5.723L0.031,5.848L0,6l0.031,0.152" +
    "l0.082,0.125l1.606,1.605c0.152,0.156,0.406,0.156,0.562,0c0.157-0.155,0.157-0.409,0-0.566L1.367,6.398h4.235v4.234L4.684,9.719" +
    "c-0.157-0.156-0.411-0.156-0.567,0c-0.156,0.156-0.156,0.41,0,0.563l1.602,1.602l0.129,0.086L6,12l0.152-0.031l0.129-0.086" +
    "l1.602-1.602c0.156-0.152,0.156-0.41,0-0.563c-0.156-0.156-0.41-0.156-0.566,0l-0.918,0.914V6.398h4.235L9.719,7.316" +
    "c-0.156,0.157-0.156,0.411,0,0.566c0.156,0.156,0.41,0.156,0.563,0l1.602-1.602l0.086-0.129L12,6L11.969,5.848z M11.969,5.848\"/></g></svg>" +
"</span></button>" +


        "<button type=\"button\" class=\"btn cusIconImg\" data-method=\"rotate\" data-option=\"90\" title=\"Rotate Right\">" +
                            "<span class=\"docs-tooltip\" data-toggle=\"tooltip\" title=\"$().cropper(&quot;rotate&quot;, 90)\">" +
                               "<svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\"" +
    "width=\"12px\" height=\"12px\" viewBox=\"0 0 18.728 20\" enable-background=\"new 0 0 18.728 20\" xml:space=\"preserve\">" +
    "<g><g><g>" +
    "<path fill=\"#666766\" d=\"M9.345,20c0.41,0,0.824-0.027,1.233-0.081c2.479-0.324,4.685-1.595,6.209-3.578" +
    "c1.484-1.932,2.152-4.329,1.881-6.749c-0.045-0.393-0.375-0.689-0.77-0.689c-0.029,0-0.059,0.001-0.088,0.005" +
    "c-0.207,0.023-0.391,0.125-0.521,0.287s-0.188,0.365-0.165,0.571c0.227,2.019-0.33,4.018-1.568,5.629" +
    "c-1.271,1.654-3.111,2.714-5.18,2.985c-0.341,0.045-0.688,0.067-1.029,0.067c-1.726,0-3.365-0.56-4.744-1.619" +
    "c-0.475-0.365-0.906-0.782-1.283-1.24c-0.923-1.122-1.511-2.483-1.701-3.939C1.349,9.581,1.9,7.53,3.17,5.876" +
    "c1.272-1.654,3.111-2.715,5.18-2.985c0.341-0.044,0.687-0.067,1.027-0.067c1.62,0,3.177,0.499,4.505,1.443l-0.718,0.718" +
    "c-0.105,0.105-0.146,0.212-0.114,0.301c0.033,0.089,0.133,0.145,0.281,0.158l2.55,0.23c0.017,0.001,0.032,0.002,0.049,0.002" +
    "c0.134,0,0.253-0.051,0.337-0.143c0.086-0.094,0.127-0.222,0.113-0.359l-0.232-2.55c-0.018-0.182-0.1-0.291-0.221-0.291" +
    "c-0.074,0-0.157,0.043-0.238,0.125l-0.696,0.697c-1.637-1.232-3.577-1.884-5.611-1.884c-0.409,0-0.824,0.027-1.233,0.081" +
    "C5.669,1.677,3.464,2.948,1.94,4.931c-1.523,1.983-2.184,4.44-1.859,6.92c0.229,1.746,0.934,3.378,2.039,4.722" +
    "c0.452,0.549,0.97,1.049,1.539,1.486C5.311,19.329,7.278,20,9.345,20L9.345,20z\"/>" +
    "</g><g>" +
    "<path fill=\"#666766\" d=\"M1.96,4.946c-1.52,1.978-2.179,4.429-1.855,6.902c0.23,1.756,0.936,3.375,2.034,4.709" +
    "c0.448,0.545,0.962,1.042,1.535,1.482c1.978,1.52,4.429,2.179,6.9,1.855c2.474-0.324,4.673-1.591,6.193-3.568" +
    "c1.48-1.927,2.146-4.317,1.875-6.73c-0.046-0.412-0.418-0.709-0.83-0.663c-0.412,0.046-0.709,0.418-0.662,0.83" +
    "c0.227,2.025-0.332,4.031-1.574,5.648c-1.275,1.659-3.121,2.723-5.196,2.994c-2.076,0.272-4.132-0.281-5.792-1.557" +
    "c-0.481-0.37-0.912-0.787-1.288-1.244c-0.922-1.12-1.514-2.478-1.707-3.952C1.323,9.577,1.875,7.521,3.151,5.861" +
    "c1.275-1.66,3.121-2.724,5.197-2.995c1.985-0.26,3.954,0.237,5.574,1.398l-0.737,0.739c-0.203,0.203-0.136,0.391,0.15,0.417" +
    "l2.55,0.23c0.286,0.025,0.499-0.188,0.473-0.473l-0.233-2.55c-0.026-0.286-0.213-0.354-0.416-0.15l-0.711,0.712" +
    "c-1.969-1.488-4.395-2.132-6.844-1.812C5.679,1.701,3.48,2.968,1.96,4.946L1.96,4.946z M1.96,4.946\"/>" +
    "</g></g></g><g>" +
    "<polygon fill=\"#666766\" points=\"16.479,6.267 15.741,3.51 15.002,0.753 12.985,2.771 10.966,4.79 13.723,5.528 	\"/>" +
    "<path fill=\"#666766\" d=\"M16.285,6.995l-5.514-1.478c-0.26-0.07-0.463-0.273-0.533-0.533c-0.07-0.26,0.005-0.538,0.195-0.728" +
    "l4.036-4.036c0.19-0.19,0.468-0.265,0.729-0.195c0.26,0.07,0.463,0.272,0.532,0.533l1.478,5.514" +
    "c0.069,0.26-0.005,0.537-0.195,0.728c-0.143,0.143-0.336,0.221-0.533,0.221C16.415,7.021,16.349,7.012,16.285,6.995z M12.422,4.4" +
    "l2.992,0.802l-0.803-2.993L12.422,4.4z\"/></g></svg>" +
                            "</span>" +
                        "</button>" +
                         "<button onclick='Ok(this)' title=\"Crop\" data-method=\"getCroppedCanvas\" class=\"btn cusIconImg cbtn-info-ok\" type=\"button\">" +
        "<span title=\"\" data-toggle=\"tooltip\" class=\"docs-tooltip\" data-original-title=\"$().cropper(&quot;crop&quot;)\">" +

"<svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" width=\"12px\" height=\"12px\" viewBox=\"0 0 27 20.168\" enable-background=\"new 0 0 27 20.168\" xml:space=\"preserve\">" +
"<polyline fill=\"none\" stroke=\"#FFFFFF\" stroke-width=\"7\" stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-miterlimit=\"10\" points=\" 3.5,9.837 10.331,16.668 23.5,3.5 \"/>" +
"</svg>" +
"</span></button>" +
                         "<div onclick='DeleteUpload(this)' title=\"Delete\" class=\"btn cusIconImg cusbtn cusbtn-del\">" +
    "<svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" width=\"12px\" height=\"12px\" viewBox=\"0 0 15.809 20\" enable-background=\"new 0 0 15.809 20\" xml:space=\"preserve\"><g>" +
    "<path fill=\"#FFFFFF\" d=\"M14.444,2.354h-2.67V0.62c0-0.35-0.281-0.62-0.631-0.62c-0.034,0-0.057,0.012-0.067,0.022" +
    "C11.064,0.012,11.042,0,11.03,0H4.664c-0.349,0-0.62,0.27-0.62,0.62v1.735H1.364C0.597,2.354,0,2.952,0,3.718V5.95h1.183v12.698" +
    "C1.183,19.414,1.769,20,2.535,20h10.738c0.767,0,1.362-0.586,1.362-1.353V5.95h1.173V3.718C15.809,2.952,15.211,2.354,14.444,2.354" +
    "L14.444,2.354z M5.284,1.239h5.239v1.115H5.284V1.239z M13.386,18.647c0,0.079-0.034,0.113-0.112,0.113H2.535" +
    "c-0.079,0-0.113-0.034-0.113-0.113V5.95h10.963V18.647z M14.558,4.71H1.251V3.718c0-0.079,0.033-0.125,0.113-0.125h13.081" +
    "c0.079,0,0.113,0.046,0.113,0.125V4.71z M14.558,4.71\"/>" +
    "<rect x=\"10.175\" y=\"7.402\" fill=\"#FFFFFF\" width=\"1.25\" height=\"10.254\"/>" +
    "<rect x=\"7.29\" y=\"7.402\" fill=\"#FFFFFF\" width=\"1.251\" height=\"10.254\"/>" +
"<rect x=\"4.406\" y=\"7.402\" fill=\"#FFFFFF\" width=\"1.25\" height=\"10.254\"/></g></svg></div>" +
                        "</div>" +
        "</div>" +
         "<div class=\"containerUpload cusdefault\"><input type=\"radio\" checked=\"checked\" name='groupimg' class=\"reset-data cusradio\" data-defalut-enabled=\"false\"/>Chọn ảnh này làm đại diện</div>" +
        " <div class=\"dz-image containerUpload\"  id=\"imgmain\"><img id=\"cusimg\"   class=\"ghost cussimg\" rel=\"0\" data-dz-thumbnail /></div>\n  " +
        "<div class=\"dz-filename cusfilename\"><input type=\"text\" placeholder=\"Tiêu đề hình ảnh\" class=\"col-xs-12 form-control valid cusinput\" id=\"txtImage\" value=\"\" name=\"\"> </div>" +
        "<div class=\"input-group input-group-sm hidden\"> " +
        "<input type=\"text\" class=\"form-control\" id=\"dataX\" placeholder=\"x\">" +
        "<input type=\"text\" class=\"form-control\" id=\"dataY\" placeholder=\"y\">" +
        "<input id=\"dataHeight\" class=\"form-control\" type=\"text\" placeholder=\"height\">" +
        "<input id=\"dataRotate\" class=\"form-control\" type=\"text\" placeholder=\"rotate\">" +
        "<input type=\"text\" class=\"form-control\" id=\"dataWidth\" placeholder=\"width\">" +


         " <div class=\"form-control\"  id=\"dataimg\"></div>" +

        "</div>" +
        " <div class=\"dz-progress\"><span class=\"dz-upload\" data-dz-uploadprogress></span></div>\n" +
        "<div class=\"dz-error-message\"><span data-dz-errormessage></span></div>\n  " +
        "<div id=\"cussucess\"  class=\"dz-success-mark\">\n <svg width=\"54px\" height=\"54px\" viewBox=\"0 0 54 54\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:sketch=\"http://www.bohemiancoding.com/sketch/ns\">\n      <title>Check</title>\n      <defs></defs>\n      <g id=\"Page-1\" stroke=\"none\" stroke-width=\"1\" fill=\"none\" fill-rule=\"evenodd\" sketch:type=\"MSPage\">\n   " +
        "<path d=\"M23.5,31.8431458 L17.5852419,25.9283877 C16.0248253,24.3679711 13.4910294,24.366835 11.9289322,25.9289322 C10.3700136,27.4878508 10.3665912,30.0234455 11.9283877,31.5852419 L20.4147581,40.0716123 C20.5133999,40.1702541 20.6159315,40.2626649 20.7218615,40.3488435 C22.2835669,41.8725651 24.794234,41.8626202 26.3461564,40.3106978 L43.3106978,23.3461564 C44.8771021,21.7797521 44.8758057,19.2483887 43.3137085,17.6862915 C41.7547899,16.1273729 39.2176035,16.1255422 37.6538436,17.6893022 L23.5,31.8431458 Z M27,53 C41.3594035,53 53,41.3594035 53,27 C53,12.6405965 41.3594035,1 27,1 C12.6405965,1 1,12.6405965 1,27 C1,41.3594035 12.6405965,53 27,53 Z\" id=\"Oval-2\" stroke-opacity=\"0.198794158\" stroke=\"#747474\" fill-opacity=\"0.816519475\" fill=\"#FFFFFF\" sketch:type=\"MSShapeGroup\"></path>\n      </g>\n    </svg>\n  " +
        "</div>\n  <div class=\"dz-error-mark\">\n    <svg width=\"54px\" height=\"54px\" viewBox=\"0 0 54 54\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:sketch=\"http://www.bohemiancoding.com/sketch/ns\">\n      <title>Error</title>\n      <defs></defs>\n      <g id=\"Page-1\" stroke=\"none\" stroke-width=\"1\" fill=\"none\" fill-rule=\"evenodd\" sketch:type=\"MSPage\">\n        <g id=\"Check-+-Oval-2\" sketch:type=\"MSLayerGroup\" stroke=\"#747474\" stroke-opacity=\"0.198794158\" fill=\"#FFFFFF\" fill-opacity=\"0.816519475\">\n          <path d=\"M32.6568542,29 L38.3106978,23.3461564 C39.8771021,21.7797521 39.8758057,19.2483887 38.3137085,17.6862915 C36.7547899,16.1273729 34.2176035,16.1255422 32.6538436,17.6893022 L27,23.3431458 L21.3461564,17.6893022 C19.7823965,16.1255422 17.2452101,16.1273729 15.6862915,17.6862915 C14.1241943,19.2483887 14.1228979,21.7797521 15.6893022,23.3461564 L21.3431458,29 L15.6893022,34.6538436 C14.1228979,36.2202479 14.1241943,38.7516113 15.6862915,40.3137085 C17.2452101,41.8726271 19.7823965,41.8744578 21.3461564,40.3106978 L27,34.6568542 L32.6538436,40.3106978 C34.2176035,41.8744578 36.7547899,41.8726271 38.3137085,40.3137085 C39.8758057,38.7516113 39.8771021,36.2202479 38.3106978,34.6538436 L32.6568542,29 Z M27,53 C41.3594035,53 53,41.3594035 53,27 C53,12.6405965 41.3594035,1 27,1 C12.6405965,1 1,12.6405965 1,27 C1,41.3594035 12.6405965,53 27,53 Z\" id=\"Oval-2\" sketch:type=\"MSShapeGroup\"></path>\n        </g>\n      </g>\n    </svg>\n  </div>\n</div>" +
        "<div class=\"dz-details\">\n    <div class=\"dz-size\"><span data-dz-size></span></div>\n\n </div>\n ";

    //"<input type=\"text\" class=\"form-control\" id=\"dataimg\" placeholder=\"tu\">" +
    var Dropzone,
        Emitter,
        camelize,
        contentLoaded,
        detectVerticalSquash,
        drawImageIOSFix,
        noop,
        without,
        custools,
        __slice = [].slice,
        __hasProp = {}.hasOwnProperty,
        __extends = function (child, parent) {
            for (var key in parent) {
                if (__hasProp.call(parent, key)) child[key] = parent[key];
            }

            function ctor() { this.constructor = child; }

            ctor.prototype = parent.prototype;
            child.prototype = new ctor();
            child.__super__ = parent.prototype;
            return child;
        };


    noop = function () { };

    Emitter = (function () {
        function Emitter() { }

        Emitter.prototype.addEventListener = Emitter.prototype.on;

        Emitter.prototype.on = function (event, fn) {
            this._callbacks = this._callbacks || {};
            if (!this._callbacks[event]) {
                this._callbacks[event] = [];
            }
            this._callbacks[event].push(fn);
            return this;
        };

        Emitter.prototype.emit = function () {
            var args, callback, callbacks, event, _i, _len;
            event = arguments[0], args = 2 <= arguments.length ? __slice.call(arguments, 1) : [];
            this._callbacks = this._callbacks || {};
            callbacks = this._callbacks[event];
            if (callbacks) {
                for (_i = 0, _len = callbacks.length; _i < _len; _i++) {
                    callback = callbacks[_i];
                    callback.apply(this, args);
                }
            }
            return this;
        };

        Emitter.prototype.removeListener = Emitter.prototype.off;

        Emitter.prototype.removeAllListeners = Emitter.prototype.off;

        Emitter.prototype.removeEventListener = Emitter.prototype.off;

        Emitter.prototype.off = function (event, fn) {
            var callback, callbacks, i, _i, _len;
            if (!this._callbacks || arguments.length === 0) {
                this._callbacks = {};
                return this;
            }
            callbacks = this._callbacks[event];
            if (!callbacks) {
                return this;
            }
            if (arguments.length === 1) {
                delete this._callbacks[event];
                return this;
            }
            for (i = _i = 0, _len = callbacks.length; _i < _len; i = ++_i) {
                callback = callbacks[i];
                if (callback === fn) {
                    callbacks.splice(i, 1);
                    break;
                }
            }
            return this;
        };

        return Emitter;

    })();

    Dropzone = (function (_super) {
        var extend, resolveOption;

        __extends(Dropzone, _super);

        Dropzone.prototype.Emitter = Emitter;


        /*
    This is a list of all available events you can register on a dropzone object.
    
    You can register an event handler like this:
    
        dropzone.on("dragEnter", function() { });
     */

        Dropzone.prototype.events = ["drop", "dragstart", "dragend", "dragenter", "dragover", "dragleave", "addedfile", "addedfiles", "removedfile", "thumbnail", "error", "errormultiple", "processing", "processingmultiple", "uploadprogress", "totaluploadprogress", "sending", "sendingmultiple", "success", "successmultiple", "canceled", "canceledmultiple", "complete", "completemultiple", "reset", "maxfilesexceeded", "maxfilesreached", "queuecomplete"];

        Dropzone.prototype.defaultOptions = {
            //url: null,
            //method: "post",
            //withCredentials: false,
            //parallelUploads: 2,
            //uploadMultiple: false,
            //maxFilesize: 2,
            //paramName: "file",
            //createImageThumbnails: true,
            //maxThumbnailFilesize: 2,
            ////thumbnailWidth: 288,
            ////thumbnailHeight: 180,
            //filesizeBase: 1000,
            //maxFiles: null,
            //params: {},
            //clickable: true,
            //ignoreHiddenFiles: true,
            //acceptedFiles: null,
            //acceptedMimeTypes: null,
            //autoProcessQueue: true,
            //autoQueue: true,
            //addRemoveLinks: false,
            //previewsContainer: null,
            //capture: null,
            //dictDefaultMessage: "Chọn file để tải lên",
            //dictFallbackMessage: "Your browser does not support drag'n'drop file uploads.",
            //dictFallbackText: "Please use the fallback form below to upload your files like in the olden days.",
            //dictFileTooBig: "File is too big ({{filesize}}MiB). Max filesize: {{maxFilesize}}MiB.",
            //dictFilesmall: "Upload Hình Có File Lớn hơn",
            //dictInvalidFileType: "You can't upload files of this type.",
            //dictResponseError: "Server responded with {{statusCode}} code.",
            //dictCancelUpload: "Cancel upload",
            //dictCancelUploadConfirmation: "Are you sure you want to cancel this upload?",
            //dictRemoveFile: "Remove file",
            //dictRemoveFileConfirmation: null,
            //dictMaxFilesExceeded: "You can not upload any more files.",

            url: null,
            method: "post",
            withCredentials: false,
            parallelUploads: 1,
            uploadMultiple: false,
            maxFilesize: 10,

            paramName: "file",
            createImageThumbnails: true,
            maxThumbnailFilesize: 2,
            //thumbnailWidth: 615,
            //thumbnailHeight: 800,

            filesizeBase: 1024,
            maxFiles: 20,
            params: {},
            clickable: true,

            ignoreHiddenFiles: true,
            acceptedFiles: null,
            acceptedMimeTypes: null,
            autoProcessQueue: true,
            autoQueue: true,
            addRemoveLinks: false,
            previewsContainer: null,
            hiddenInputContainer: "body",
            capture: null,
            renameFilename: null,
            dictDefaultMessage: "Drop files here to upload",
            dictFallbackMessage: "Your browser does not support drag'n'drop file uploads.",
            dictFallbackText: "Please use the fallback form below to upload your files like in the olden days.",
            dictFileTooBig: "File is too big ({{filesize}}MiB). Max filesize: {{maxFilesize}}MiB.",
            dictInvalidFileType: "You can't upload files of this type.",
            dictResponseError: "Server responded with {{statusCode}} code.",
            dictCancelUpload: "Cancel upload",
            dictCancelUploadConfirmation: "Are you sure you want to cancel this upload?",
            dictRemoveFile: "Remove file",
            dictRemoveFileConfirmation: null,
            dictMaxFilesExceeded: "You can not upload any more files.",
            accept: function (file, done) {

                return done();
            },
            init: function () {
                return noop;
            },
            forceFallback: false,
            fallback: function () {
                var child, messageElement, span, _i, _len, _ref;
                this.element.className = "" + this.element.className + " dz-browser-not-supported";
                _ref = this.element.getElementsByTagName("div");
                for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                    child = _ref[_i];
                    if (/(^| )dz-message($| )/.test(child.className)) {
                        messageElement = child;
                        child.className = "dz-message";
                        continue;
                    }
                }
                if (!messageElement) {
                    messageElement = Dropzone.createElement("<div class=\"dz-message\"><span></span></div>");
                    this.element.appendChild(messageElement);
                }
                span = messageElement.getElementsByTagName("span")[0];
                if (span) {
                    if (span.textContent != null) {
                        span.textContent = this.options.dictFallbackMessage;
                    } else if (span.innerText != null) {
                        span.innerText = this.options.dictFallbackMessage;
                    }
                }
                return this.element.appendChild(this.getFallbackForm());
            },
            resize: function (file) {

                var info, srcRatio, trgRatio;
                info = {
                    srcX: 0,
                    srcY: 0,
                    srcWidth: file.width,
                    srcHeight: file.height
                };
                srcRatio = file.width / file.height;
                info.optWidth = this.options.thumbnailWidth;
                info.optHeight = this.options.thumbnailHeight;
                if ((info.optWidth == null) && (info.optHeight == null)) {
                    info.optWidth = info.srcWidth;
                    info.optHeight = info.srcHeight;
                } else if (info.optWidth == null) {
                    info.optWidth = srcRatio * info.optHeight;
                } else if (info.optHeight == null) {
                    info.optHeight = (1 / srcRatio) * info.optWidth;
                }
                trgRatio = info.optWidth / info.optHeight;
                if (file.height < info.optHeight || file.width < info.optWidth) {
                    info.trgHeight = info.srcHeight;
                    info.trgWidth = info.srcWidth;
                } else {
                    if (srcRatio > trgRatio) {
                        info.srcHeight = file.height;
                        info.srcWidth = info.srcHeight * trgRatio;
                    } else {
                        info.srcWidth = file.width;
                        info.srcHeight = info.srcWidth / trgRatio;
                    }
                }
                info.srcX = (file.width - info.srcWidth) / 2;
                info.srcY = (file.height - info.srcHeight) / 2;
                return info;
            },
            drop: function (e) {
                return this.element.classList.remove("dz-drag-hover");
            },
            dragstart: noop,
            dragend: function (e) {
                return this.element.classList.remove("dz-drag-hover");
            },
            dragenter: function (e) {
                return this.element.classList.add("dz-drag-hover");
            },
            dragover: function (e) {
                return this.element.classList.add("dz-drag-hover");
            },
            dragleave: function (e) {
                return this.element.classList.remove("dz-drag-hover");
            },
            paste: noop,
            reset: function () {
                return this.element.classList.remove("dz-started");
            },
            addedfile: function (file) {
                var node, removeFileEvent, removeLink, _i, _j, _k, _len, _len1, _len2, _ref, _ref1, _ref2, _results;
                if (this.element === this.previewsContainer) {
                    this.element.classList.add("dz-started");
                }
                if (this.previewsContainer) {
                    file.previewElement = Dropzone.createElement(this.options.previewTemplate.trim());
                    file.previewTemplate = file.previewElement;
                    this.previewsContainer.appendChild(file.previewElement);
                    _ref = file.previewElement.querySelectorAll("[data-dz-name]");
                    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                        node = _ref[_i];
                        node.textContent = file.name;
                    }
                    _ref1 = file.previewElement.querySelectorAll("[data-dz-size]");
                    for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
                        node = _ref1[_j];
                        node.innerHTML = this.filesize(file.size);
                    }
                    if (this.options.addRemoveLinks) {
                        file._removeLink = Dropzone.createElement("<a class=\"dz-remove\" href=\"javascript:undefined;\" data-dz-remove>" + this.options.dictRemoveFile + "</a>");
                        file.previewElement.appendChild(file._removeLink);
                    }
                    removeFileEvent = (function (_this) {
                        return function (e) {
                            e.preventDefault();
                            e.stopPropagation();
                            if (file.status === Dropzone.UPLOADING) {
                                return Dropzone.confirm(_this.options.dictCancelUploadConfirmation, function () {
                                    return _this.removeFile(file);
                                });
                            } else {
                                if (_this.options.dictRemoveFileConfirmation) {
                                    return Dropzone.confirm(_this.options.dictRemoveFileConfirmation, function () {
                                        return _this.removeFile(file);
                                    });
                                } else {
                                    return _this.removeFile(file);
                                }
                            }
                        };
                    })(this);
                    _ref2 = file.previewElement.querySelectorAll("[data-dz-remove]");
                    _results = [];
                    for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
                        removeLink = _ref2[_k];
                        _results.push(removeLink.addEventListener("click", removeFileEvent));
                    }
                    return _results;
                }
            },
            removedfile: function (file) {
                var _ref;
                if (file.previewElement) {
                    if ((_ref = file.previewElement) != null) {
                        _ref.parentNode.removeChild(file.previewElement);
                    }
                }
                return this._updateMaxFilesReachedClass();
            },
            thumbnail: function (file, dataUrl) {
                var thumbnailElement, _i, _len, _ref;
                if (file.previewElement) {
                    file.previewElement.classList.remove("dz-file-preview");
                    _ref = file.previewElement.querySelectorAll("[data-dz-thumbnail]");
                    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                        thumbnailElement = _ref[_i];
                        thumbnailElement.alt = file.name;
                        thumbnailElement.src = dataUrl;
                    }
                    return setTimeout(((function (_this) {
                        return function () {
                            return file.previewElement.classList.add("dz-image-preview");
                        };
                    })(this)), 1);
                }
            },
            error: function (file, message) {
                var node, _i, _len, _ref, _results;

                if (file.previewElement) {
                    file.previewElement.classList.add("dz-error");
                    if (typeof message !== "String" && message.error) {
                        message = message.error;
                    }
                    _ref = file.previewElement.querySelectorAll("[data-dz-errormessage]");
                    _results = [];
                    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                        node = _ref[_i];
                        _results.push(node.textContent = "Không thể Upload vì file quá nhỏ hoặc quá lớn");
                    }
                    return _results;
                }
            },
            errormultiple: noop,
            processing: function (file) {
                if (file.previewElement) {

                    file.previewElement.classList.add("dz-processing");
                    if (file._removeLink) {
                        return file._removeLink.textContent = this.options.dictCancelUpload;
                    }
                }
            },
            processingmultiple: noop,
            uploadprogress: function (file, progress, bytesSent) {
                var node, _i, _len, _ref, _results;

                if (file.previewElement) {
                    _ref = file.previewElement.querySelectorAll("[data-dz-uploadprogress]");
                    _results = [];
                    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                        node = _ref[_i];
                        if (node.nodeName === 'PROGRESS') {

                            _results.push(node.value = progress);


                        } else {
                            var timerfindcedit1 = setInterval(function () {
                                if (progress < 100) {
                                    $('.btnNextNewUi').addClass('disabled');
                                    $('.btnNextNewUi').text('Đang xử lý...');
                                    console.log(progress);
                                } else {
                                    _results.push(node.style.width = "" + progress + "%");
                                    $('.btnNextNewUi').removeClass('disabled');
                                    $('.btnNextNewUi').text('Bước tiếp theo');
                                    console.log(progress);
                                }
                                clearInterval(timerfindcedit1);
                            }, 100);
                           

                        }

                    }

                    return _results;
                }
            },

            totaluploadprogress: noop,
            sending: noop,
            sendingmultiple: noop,
            success: function (file) {
                if (file.previewElement) {

                    return file.previewElement.classList.add("dz-success");
                }
            },
            successmultiple: noop,
            canceled: function (file) {
                return this.emit("error", file, "Upload canceled.");
            },
            canceledmultiple: noop,
            complete: function (file) {

                if (file._removeLink) {
                    file._removeLink.textContent = this.options.dictRemoveFile;
                }
                if (file.previewElement) {
                    if (file.size > 2000000) {


                        var timerfindcedit1 = setInterval(function () {
                            $('.dz-error').remove();
                            toastr.error('Kích thước hình ' + file.name + ' lớn hơn 2MB. Vui lòng chọn ảnh khác');
                            clearInterval(timerfindcedit1);
                        }, 100);

                        return this.emit("error", file, "File quá nhỏ");
                    }
                    if (file.height < 350 && (file.height * 3 / 4)) {
                        var timerfindcedit2 = setInterval(function () {
                            $('.dz-error').remove();
                            toastr.error('Kích thước hình ' + file.name + ' không đúng');
                            clearInterval(timerfindcedit2);
                        }, 100);
                        return this.emit("error", file, "File quá nhỏ");
                    }
                    var timerfindcedit = setInterval(function () {
                        if ($(file.previewElement).find('.cbtn-edit').size() != 0) {
                            $(file.previewElement).find('.cbtn-edit').click();
                            $('.dropzone').each(function () {
                                $('input[type=radio]', this).get(0).checked = true;


                            });
                            clearInterval(timerfindcedit);
                        }

                    }, 100);
                    $('.btnNextNewUi').removeClass('disabled');
                    return file.previewElement.classList.add("dz-complete");


                }

            },
            completemultiple: noop,
            maxfilesexceeded: noop,
            maxfilesreached: noop,
            queuecomplete: noop,
            addedfiles: noop,
            previewTemplate: content


        };


        extend = function () {
            var key, object, objects, target, val, _i, _len;
            target = arguments[0], objects = 2 <= arguments.length ? __slice.call(arguments, 1) : [];
            for (_i = 0, _len = objects.length; _i < _len; _i++) {
                object = objects[_i];
                for (key in object) {
                    val = object[key];
                    target[key] = val;
                }
            }
            return target;
        };

        function Dropzone(element, options) {
            var elementOptions, fallback, _ref;
            this.element = element;
            this.version = Dropzone.version;
            this.defaultOptions.previewTemplate = this.defaultOptions.previewTemplate.replace(/\n*/g, "");
            this.clickableElements = [];
            this.listeners = [];
            this.files = [];
            if (typeof this.element === "string") {
                this.element = document.querySelector(this.element);
            }
            if (!(this.element && (this.element.nodeType != null))) {
                throw new Error("Invalid dropzone element.");
            }
            if (this.element.dropzone) {
                throw new Error("Dropzone already attached.");
            }
            Dropzone.instances.push(this);
            this.element.dropzone = this;
            elementOptions = (_ref = Dropzone.optionsForElement(this.element)) != null ? _ref : {};
            this.options = extend({}, this.defaultOptions, elementOptions, options != null ? options : {});
            if (this.options.forceFallback || !Dropzone.isBrowserSupported()) {
                return this.options.fallback.call(this);
            }
            if (this.options.url == null) {
                this.options.url = this.element.getAttribute("action");
            }
            if (!this.options.url) {
                throw new Error("No URL provided.");
            }
            if (this.options.acceptedFiles && this.options.acceptedMimeTypes) {
                throw new Error("You can't provide both 'acceptedFiles' and 'acceptedMimeTypes'. 'acceptedMimeTypes' is deprecated.");
            }
            if (this.options.acceptedMimeTypes) {
                this.options.acceptedFiles = this.options.acceptedMimeTypes;
                delete this.options.acceptedMimeTypes;
            }
            this.options.method = this.options.method.toUpperCase();
            if ((fallback = this.getExistingFallback()) && fallback.parentNode) {
                fallback.parentNode.removeChild(fallback);
            }
            if (this.options.previewsContainer !== false) {
                if (this.options.previewsContainer) {
                    this.previewsContainer = Dropzone.getElement(this.options.previewsContainer, "previewsContainer");
                } else {
                    this.previewsContainer = this.element;
                }
            }
            if (this.options.clickable) {
                if (this.options.clickable === true) {
                    this.clickableElements = [this.element];
                } else {
                    this.clickableElements = Dropzone.getElements(this.options.clickable, "clickable");
                }
            }
            this.init();
        }

        Dropzone.prototype.getAcceptedFiles = function () {
            var file, _i, _len, _ref, _results;
            _ref = this.files;
            _results = [];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                file = _ref[_i];
                if (file.accepted) {
                    _results.push(file);
                }
            }
            return _results;
        };

        Dropzone.prototype.getRejectedFiles = function () {
            var file, _i, _len, _ref, _results;
            _ref = this.files;
            _results = [];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                file = _ref[_i];
                if (!file.accepted) {
                    _results.push(file);
                }
            }
            return _results;
        };

        Dropzone.prototype.getFilesWithStatus = function (status) {
            var file, _i, _len, _ref, _results;
            _ref = this.files;
            _results = [];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                file = _ref[_i];
                if (file.status === status) {
                    _results.push(file);
                }
            }
            return _results;
        };

        Dropzone.prototype.getQueuedFiles = function () {
            return this.getFilesWithStatus(Dropzone.QUEUED);
        };

        Dropzone.prototype.getUploadingFiles = function () {
            return this.getFilesWithStatus(Dropzone.UPLOADING);
        };

        Dropzone.prototype.getAddedFiles = function () {
            return this.getFilesWithStatus(Dropzone.ADDED);
        };

        Dropzone.prototype.getActiveFiles = function () {
            var file, _i, _len, _ref, _results;
            _ref = this.files;
            _results = [];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                file = _ref[_i];
                if (file.status === Dropzone.UPLOADING || file.status === Dropzone.QUEUED) {
                    _results.push(file);
                }
            }
            return _results;
        };

        Dropzone.prototype.init = function () {
            var eventName, noPropagation, setupHiddenFileInput, _i, _len, _ref, _ref1;
            if (this.element.tagName === "form") {
                this.element.setAttribute("enctype", "multipart/form-data");
            }
            if (this.element.classList.contains("dropzone") && !this.element.querySelector(".dz-message")) {
                this.element.appendChild(Dropzone.createElement("<div style=\"display: block;\" class=\"dz-default dz-message cusbuttonmes\"><span class=\"btn btn-action\">" + this.options.dictDefaultMessage + "</span></div>"));
            }
            if (this.clickableElements.length) {
                setupHiddenFileInput = (function (_this) {
                    return function () {
                        if (_this.hiddenFileInput) {
                            document.body.removeChild(_this.hiddenFileInput);
                        }
                        _this.hiddenFileInput = document.createElement("input");
                        _this.hiddenFileInput.setAttribute("type", "file");
                        if ((_this.options.maxFiles == null) || _this.options.maxFiles > 1) {
                            _this.hiddenFileInput.setAttribute("multiple", "multiple");
                        }
                        _this.hiddenFileInput.className = "dz-hidden-input";
                        if (_this.options.acceptedFiles != null) {
                            _this.hiddenFileInput.setAttribute("accept", _this.options.acceptedFiles);
                        }
                        if (_this.options.capture != null) {
                            _this.hiddenFileInput.setAttribute("capture", _this.options.capture);
                        }
                        _this.hiddenFileInput.style.visibility = "hidden";
                        _this.hiddenFileInput.style.position = "absolute";
                        _this.hiddenFileInput.style.top = "0";
                        _this.hiddenFileInput.style.left = "0";
                        _this.hiddenFileInput.style.height = "0";
                        _this.hiddenFileInput.style.width = "0";
                        document.body.appendChild(_this.hiddenFileInput);
                        return _this.hiddenFileInput.addEventListener("change", function () {
                            var file, files, _i, _len;
                            files = _this.hiddenFileInput.files;
                            if (files.length) {

                                for (_i = 0, _len = files.length; _i < _len; _i++) {
                                    file = files[_i];
                                    _this.addFile(file);
                                }


                            }
                            _this.emit("addedfiles", files);
                            return setupHiddenFileInput();
                        });
                    };
                })(this);
                setupHiddenFileInput();
            }
            this.URL = (_ref = window.URL) != null ? _ref : window.webkitURL;
            _ref1 = this.events;
            for (_i = 0, _len = _ref1.length; _i < _len; _i++) {
                eventName = _ref1[_i];
                this.on(eventName, this.options[eventName]);
            }
            this.on("uploadprogress", (function (_this) {
                return function () {
                    return _this.updateTotalUploadProgress();
                };
            })(this));
            this.on("removedfile", (function (_this) {
                return function () {
                    return _this.updateTotalUploadProgress();
                };
            })(this));
            this.on("canceled", (function (_this) {
                return function (file) {
                    return _this.emit("complete", file);
                };
            })(this));
            this.on("complete", (function (_this) {
                return function (file) {
                    if (_this.getAddedFiles().length === 0 && _this.getUploadingFiles().length === 0 && _this.getQueuedFiles().length === 0) {
                        return setTimeout((function () {
                            return _this.emit("queuecomplete");
                        }), 0);
                    }
                };
            })(this));
            noPropagation = function (e) {
                e.stopPropagation();
                if (e.preventDefault) {
                    return e.preventDefault();
                } else {
                    return e.returnValue = false;
                }
            };
            this.listeners = [
                {
                    element: this.element,
                    events: {
                        "dragstart": (function (_this) {
                            return function (e) {
                                return _this.emit("dragstart", e);
                            };
                        })(this),
                        "dragenter": (function (_this) {
                            return function (e) {
                                noPropagation(e);
                                return _this.emit("dragenter", e);
                            };
                        })(this),
                        "dragover": (function (_this) {
                            return function (e) {
                                var efct;
                                try {
                                    efct = e.dataTransfer.effectAllowed;
                                } catch (_error) {
                                }
                                e.dataTransfer.dropEffect = 'move' === efct || 'linkMove' === efct ? 'move' : 'copy';
                                noPropagation(e);
                                return _this.emit("dragover", e);
                            };
                        })(this),
                        "dragleave": (function (_this) {
                            return function (e) {
                                return _this.emit("dragleave", e);
                            };
                        })(this),
                        "drop": (function (_this) {
                            return function (e) {
                                noPropagation(e);
                                return _this.drop(e);
                            };
                        })(this),
                        "dragend": (function (_this) {
                            return function (e) {
                                return _this.emit("dragend", e);
                            };
                        })(this)
                    }
                }
            ];
            this.clickableElements.forEach((function (_this) {
                return function (clickableElement) {
                    return _this.listeners.push({
                        element: clickableElement,
                        events: {
                            "click": function (evt) {
                                if ((clickableElement !== _this.element) || (evt.target === _this.element || Dropzone.elementInside(evt.target, _this.element.querySelector(".dz-message")))) {
                                    _this.hiddenFileInput.click();
                                }
                                return true;
                            }
                        }
                    });
                };
            })(this));
            this.enable();
            return this.options.init.call(this);
        };

        Dropzone.prototype.destroy = function () {
            var _ref;
            this.disable();
            this.removeAllFiles(true);
            if ((_ref = this.hiddenFileInput) != null ? _ref.parentNode : void 0) {
                this.hiddenFileInput.parentNode.removeChild(this.hiddenFileInput);
                this.hiddenFileInput = null;
            }
            delete this.element.dropzone;
            return Dropzone.instances.splice(Dropzone.instances.indexOf(this), 1);
        };

        Dropzone.prototype.updateTotalUploadProgress = function () {
            var activeFiles, file, totalBytes, totalBytesSent, totalUploadProgress, _i, _len, _ref;
            totalBytesSent = 0;
            totalBytes = 0;
            activeFiles = this.getActiveFiles();
            if (activeFiles.length) {
                _ref = this.getActiveFiles();
                for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                    file = _ref[_i];
                    totalBytesSent += file.upload.bytesSent;
                    totalBytes += file.upload.total;
                }
                totalUploadProgress = 100 * totalBytesSent / totalBytes;
            } else {
                totalUploadProgress = 100;
            }
            return this.emit("totaluploadprogress", totalUploadProgress, totalBytes, totalBytesSent);
        };

        Dropzone.prototype._getParamName = function (n) {
            if (typeof this.options.paramName === "function") {
                return this.options.paramName(n);
            } else {
                return "" + this.options.paramName + (this.options.uploadMultiple ? "[" + n + "]" : "");
            }
        };

        Dropzone.prototype.getFallbackForm = function () {
            var existingFallback, fields, fieldsString, form;
            if (existingFallback = this.getExistingFallback()) {
                return existingFallback;
            }
            fieldsString = "<div class=\"dz-fallback\">";
            if (this.options.dictFallbackText) {
                fieldsString += "<p>" + this.options.dictFallbackText + "</p>";
            }
            fieldsString += "<input type=\"file\" name=\"" + (this._getParamName(0)) + "\" " + (this.options.uploadMultiple ? 'multiple="multiple"' : void 0) + " /><input type=\"submit\" value=\"Upload!\"></div>";
            fields = Dropzone.createElement(fieldsString);
            if (this.element.tagName !== "FORM") {
                form = Dropzone.createElement("<form action=\"" + this.options.url + "\" enctype=\"multipart/form-data\" method=\"" + this.options.method + "\"></form>");
                form.appendChild(fields);
            } else {
                this.element.setAttribute("enctype", "multipart/form-data");
                this.element.setAttribute("method", this.options.method);
            }
            return form != null ? form : fields;
        };

        Dropzone.prototype.getExistingFallback = function () {
            var fallback, getFallback, tagName, _i, _len, _ref;
            getFallback = function (elements) {
                var el, _i, _len;
                for (_i = 0, _len = elements.length; _i < _len; _i++) {
                    el = elements[_i];
                    if (/(^| )fallback($| )/.test(el.className)) {
                        return el;
                    }
                }
            };
            _ref = ["div", "form"];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                tagName = _ref[_i];
                if (fallback = getFallback(this.element.getElementsByTagName(tagName))) {
                    return fallback;
                }
            }
        };

        Dropzone.prototype.setupEventListeners = function () {
            var elementListeners, event, listener, _i, _len, _ref, _results;
            _ref = this.listeners;
            _results = [];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                elementListeners = _ref[_i];
                _results.push((function () {
                    var _ref1, _results1;
                    _ref1 = elementListeners.events;
                    _results1 = [];
                    for (event in _ref1) {
                        listener = _ref1[event];
                        _results1.push(elementListeners.element.addEventListener(event, listener, false));
                    }
                    return _results1;
                })());
            }
            return _results;
        };

        Dropzone.prototype.removeEventListeners = function () {
            var elementListeners, event, listener, _i, _len, _ref, _results;
            _ref = this.listeners;
            _results = [];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                elementListeners = _ref[_i];
                _results.push((function () {
                    var _ref1, _results1;
                    _ref1 = elementListeners.events;
                    _results1 = [];
                    for (event in _ref1) {
                        listener = _ref1[event];
                        _results1.push(elementListeners.element.removeEventListener(event, listener, false));
                    }
                    return _results1;
                })());
            }
            return _results;
        };

        Dropzone.prototype.disable = function () {
            var file, _i, _len, _ref, _results;
            this.clickableElements.forEach(function (element) {
                return element.classList.remove("dz-clickable");
            });
            this.removeEventListeners();
            _ref = this.files;
            _results = [];
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                file = _ref[_i];
                _results.push(this.cancelUpload(file));
            }
            return _results;
        };

        Dropzone.prototype.enable = function () {
            this.clickableElements.forEach(function (element) {
                return element.classList.add("dz-clickable");
            });
            return this.setupEventListeners();
        };

        Dropzone.prototype.filesize = function (size) {
            var cutoff, i, selectedSize, selectedUnit, unit, units, _i, _len;
            selectedSize = 0;
            selectedUnit = "b";
            if (size > 0) {
                units = ['TB', 'GB', 'MB', 'KB', 'b'];
                for (i = _i = 0, _len = units.length; _i < _len; i = ++_i) {
                    unit = units[i];
                    cutoff = Math.pow(this.options.filesizeBase, 4 - i) / 10;
                    if (size >= cutoff) {
                        selectedSize = size / Math.pow(this.options.filesizeBase, 4 - i);
                        selectedUnit = unit;
                        break;
                    }
                }
                selectedSize = Math.round(10 * selectedSize) / 10;
            }
            return "<strong>" + selectedSize + "</strong> " + selectedUnit;
        };

        Dropzone.prototype._updateMaxFilesReachedClass = function () {
            if ((this.options.maxFiles != null) && this.getAcceptedFiles().length >= this.options.maxFiles) {
                if (this.getAcceptedFiles().length === this.options.maxFiles) {
                    this.emit('maxfilesreached', this.files);
                }
                return this.element.classList.add("dz-max-files-reached");
            } else {
                return this.element.classList.remove("dz-max-files-reached");
            }
        };

        Dropzone.prototype.drop = function (e) {
            var files, items;
            if (!e.dataTransfer) {
                return;
            }
            this.emit("drop", e);
            files = e.dataTransfer.files;
            this.emit("addedfiles", files);
            if (files.length) {
                items = e.dataTransfer.items;
                if (items && items.length && (items[0].webkitGetAsEntry != null)) {
                    this._addFilesFromItems(items);
                } else {
                    this.handleFiles(files);
                }
            }
        };

        Dropzone.prototype.paste = function (e) {
            var items, _ref;
            if ((e != null ? (_ref = e.clipboardData) != null ? _ref.items : void 0 : void 0) == null) {
                return;
            }
            this.emit("paste", e);
            items = e.clipboardData.items;
            if (items.length) {
                return this._addFilesFromItems(items);
            }
        };

        Dropzone.prototype.handleFiles = function (files) {
            var file, _i, _len, _results;
            _results = [];
            for (_i = 0, _len = files.length; _i < _len; _i++) {
                file = files[_i];
                _results.push(this.addFile(file));
            }
            return _results;
        };

        Dropzone.prototype._addFilesFromItems = function (items) {
            var entry, item, _i, _len, _results;
            _results = [];
            for (_i = 0, _len = items.length; _i < _len; _i++) {
                item = items[_i];
                if ((item.webkitGetAsEntry != null) && (entry = item.webkitGetAsEntry())) {
                    if (entry.isFile) {
                        _results.push(this.addFile(item.getAsFile()));
                    } else if (entry.isDirectory) {
                        _results.push(this._addFilesFromDirectory(entry, entry.name));
                    } else {
                        _results.push(void 0);
                    }
                } else if (item.getAsFile != null) {
                    if ((item.kind == null) || item.kind === "file") {
                        _results.push(this.addFile(item.getAsFile()));
                    } else {
                        _results.push(void 0);
                    }
                } else {
                    _results.push(void 0);
                }
            }
            return _results;
        };

        Dropzone.prototype._addFilesFromDirectory = function (directory, path) {
            var dirReader, entriesReader;
            dirReader = directory.createReader();
            entriesReader = (function (_this) {
                return function (entries) {
                    var entry, _i, _len;
                    for (_i = 0, _len = entries.length; _i < _len; _i++) {
                        entry = entries[_i];
                        if (entry.isFile) {
                            entry.file(function (file) {
                                if (_this.options.ignoreHiddenFiles && file.name.substring(0, 1) === '.') {
                                    return;
                                }
                                file.fullPath = "" + path + "/" + file.name;
                                return _this.addFile(file);
                            });
                        } else if (entry.isDirectory) {
                            _this._addFilesFromDirectory(entry, "" + path + "/" + entry.name);
                        }
                    }
                };
            })(this);
            return dirReader.readEntries(entriesReader, function (error) {
                return typeof console !== "undefined" && console !== null ? typeof console.log === "function" ? console.log(error) : void 0 : void 0;
            });
        };

        Dropzone.prototype.accept = function (file, done) {

            if (file.size > this.options.maxFilesize * 1024 * 1024) {
                return done(this.options.dictFileTooBig.replace("{{filesize}}", Math.round(file.size / 1024 / 10.24) / 100).replace("{{maxFilesize}}", this.options.maxFilesize));

            } else if (!Dropzone.isValidFile(file, this.options.acceptedFiles)) {
                return done(this.options.dictInvalidFileType);
            } else if ((this.options.maxFiles != null) && this.getAcceptedFiles().length >= this.options.maxFiles) {
                done(this.options.dictMaxFilesExceeded.replace("{{maxFiles}}", this.options.maxFiles));
                return this.emit("maxfilesexceeded", file);

            } else {
                return this.options.accept.call(this, file, done);
            }
        };

        Dropzone.prototype.addFile = function (file) {
            file.upload = {
                progress: 0,
                total: file.size,
                bytesSent: 0
            };
            this.files.push(file);
            file.status = Dropzone.ADDED;
            this.emit("addedfile", file);
            this._enqueueThumbnail(file);
            return this.accept(file, (function (_this) {
                return function (error) {
                    if (error) {
                        file.accepted = false;
                        _this._errorProcessing([file], error);
                    } else {
                        file.accepted = true;
                        if (_this.options.autoQueue) {
                            _this.enqueueFile(file);
                        }
                    }
                    return _this._updateMaxFilesReachedClass();
                };
            })(this));
        };

        Dropzone.prototype.enqueueFiles = function (files) {
            var file, _i, _len;
            for (_i = 0, _len = files.length; _i < _len; _i++) {
                file = files[_i];
                this.enqueueFile(file);
            }
            return null;
        };

        Dropzone.prototype.enqueueFile = function (file) {
            if (file.status === Dropzone.ADDED && file.accepted === true) {
                file.status = Dropzone.QUEUED;
                if (this.options.autoProcessQueue) {
                    return setTimeout(((function (_this) {
                        return function () {
                            return _this.processQueue();
                        };
                    })(this)), 0);
                }
            } else {
                throw new Error("This file can't be queued because it has already been processed or was rejected.");
            }
        };

        Dropzone.prototype._thumbnailQueue = [];

        Dropzone.prototype._processingThumbnail = false;

        Dropzone.prototype._enqueueThumbnail = function (file) {
            if (this.options.createImageThumbnails && file.type.match(/image.*/) && file.size <= this.options.maxThumbnailFilesize * 1024 * 1024) {
                this._thumbnailQueue.push(file);
                return setTimeout(((function (_this) {
                    return function () {
                        return _this._processThumbnailQueue();
                    };
                })(this)), 0);
            }
        };

        Dropzone.prototype._processThumbnailQueue = function () {
            if (this._processingThumbnail || this._thumbnailQueue.length === 0) {
                return;
            }
            this._processingThumbnail = true;
            return this.createThumbnail(this._thumbnailQueue.shift(), (function (_this) {
                return function () {
                    _this._processingThumbnail = false;
                    return _this._processThumbnailQueue();
                };
            })(this));
        };

        Dropzone.prototype.removeFile = function (file) {
            if (file.status === Dropzone.UPLOADING) {
                this.cancelUpload(file);
            }
            this.files = without(this.files, file);
            this.emit("removedfile", file);
            if (this.files.length === 0) {
                return this.emit("reset");
            }
        };

        Dropzone.prototype.removeAllFiles = function (cancelIfNecessary) {
            var file, _i, _len, _ref;
            if (cancelIfNecessary == null) {
                cancelIfNecessary = false;
            }
            _ref = this.files.slice();
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                file = _ref[_i];
                if (file.status !== Dropzone.UPLOADING || cancelIfNecessary) {
                    this.removeFile(file);
                }
            }
            return null;
        };

        Dropzone.prototype.createThumbnail = function (file, callback) {
            var fileReader;
            fileReader = new FileReader;
            fileReader.onload = (function (_this) {
                return function () {
                    if (file.type === "image/svg+xml") {
                        _this.emit("thumbnail", file, fileReader.result);
                        if (callback != null) {
                            callback();
                        }
                        return;
                    }
                    
                    return _this.createThumbnailFromUrl(file, fileReader.result, callback);
                };
            })(this);
            return fileReader.readAsDataURL(file);
        };

        Dropzone.prototype.createThumbnailFromUrl = function (file, imageUrl, callback) {

            var img;
            img = document.createElement("img");
            img.onload = (function (_this) {
                return function () {
                    var canvas, ctx, resizeInfo, thumbnail, _ref, _ref1, _ref2, _ref3;
                    file.width = img.width;
                    file.height = img.height;
                    resizeInfo = _this.options.resize.call(_this, file);

                    if (resizeInfo.trgWidth == null) {
                        resizeInfo.trgWidth = resizeInfo.optWidth;
                    }
                    if (resizeInfo.trgHeight == null) {
                        resizeInfo.trgHeight = resizeInfo.optHeight;
                    }
                    canvas = document.createElement("canvas");
                    ctx = canvas.getContext("2d");
                    canvas.width = resizeInfo.trgWidth;
                    canvas.height = resizeInfo.trgHeight;
                    drawImageIOSFix(ctx, img, (_ref = resizeInfo.srcX) != null ? _ref : 0, (_ref1 = resizeInfo.srcY) != null ? _ref1 : 0, resizeInfo.srcWidth, resizeInfo.srcHeight, (_ref2 = resizeInfo.trgX) != null ? _ref2 : 0, (_ref3 = resizeInfo.trgY) != null ? _ref3 : 0, resizeInfo.trgWidth, resizeInfo.trgHeight);
                    thumbnail = canvas.toDataURL("image/png");
                    _this.emit("thumbnail", file, thumbnail);




                    if (callback != null) {
                        return callback();
                    }
                };
            })(this);
            if (callback != null) {
                img.onerror = callback;
            }
            return img.src = imageUrl;
        };



        Dropzone.prototype.processQueue = function () {
            var i, parallelUploads, processingLength, queuedFiles;

            parallelUploads = this.options.parallelUploads;
            processingLength = this.getUploadingFiles().length;
            i = processingLength;
            if (processingLength >= parallelUploads) {
                return;
            }
            queuedFiles = this.getQueuedFiles();
            if (!(queuedFiles.length > 0)) {
                return;
            }
            if (this.options.uploadMultiple) {
                return this.processFiles(queuedFiles.slice(0, parallelUploads - processingLength));
            } else {
                while (i < parallelUploads) {
                    if (!queuedFiles.length) {
                        return;
                    }
                    this.processFile(queuedFiles.shift());
                    i++;
                }
            }
        };

        Dropzone.prototype.processFile = function (file) {
            return this.processFiles([file]);
        };

        Dropzone.prototype.processFiles = function (files) {
            var file, _i, _len;
            for (_i = 0, _len = files.length; _i < _len; _i++) {
                file = files[_i];
                file.processing = true;
                file.status = Dropzone.UPLOADING;
                this.emit("processing", file);
            }
            if (this.options.uploadMultiple) {
                this.emit("processingmultiple", files);
            }
            return this.uploadFiles(files);
        };

        Dropzone.prototype._getFilesWithXhr = function (xhr) {
            var file, files;
            return files = (function () {
                var _i, _len, _ref, _results;
                _ref = this.files;
                _results = [];
                for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                    file = _ref[_i];
                    if (file.xhr === xhr) {
                        _results.push(file);
                    }
                }
                return _results;
            }).call(this);
        };

        Dropzone.prototype.cancelUpload = function (file) {
            var groupedFile, groupedFiles, _i, _j, _len, _len1, _ref;
            if (file.status === Dropzone.UPLOADING) {
                groupedFiles = this._getFilesWithXhr(file.xhr);
                for (_i = 0, _len = groupedFiles.length; _i < _len; _i++) {
                    groupedFile = groupedFiles[_i];
                    groupedFile.status = Dropzone.CANCELED;
                }
                file.xhr.abort();
                for (_j = 0, _len1 = groupedFiles.length; _j < _len1; _j++) {
                    groupedFile = groupedFiles[_j];
                    this.emit("canceled", groupedFile);
                }
                if (this.options.uploadMultiple) {
                    this.emit("canceledmultiple", groupedFiles);
                }
            } else if ((_ref = file.status) === Dropzone.ADDED || _ref === Dropzone.QUEUED) {
                file.status = Dropzone.CANCELED;
                this.emit("canceled", file);
                if (this.options.uploadMultiple) {
                    this.emit("canceledmultiple", [file]);
                }
            }
            if (this.options.autoProcessQueue) {
                return this.processQueue();
            }
        };

        resolveOption = function () {
            var args, option;
            option = arguments[0], args = 2 <= arguments.length ? __slice.call(arguments, 1) : [];
            if (typeof option === 'function') {
                return option.apply(this, args);
            }
            return option;
        };

        Dropzone.prototype.uploadFile = function (file) {
            return this.uploadFiles([file]);
        };

        Dropzone.prototype.uploadFiles = function (files) {
            var file, formData, handleError, headerName, headerValue, headers, i, input, inputName, inputType, key, method, option, progressObj, response, updateProgress, url, value, xhr, _i, _j, _k, _l, _len, _len1, _len2, _len3, _m, _ref, _ref1, _ref2, _ref3, _ref4, _ref5;
            xhr = new XMLHttpRequest();
            for (_i = 0, _len = files.length; _i < _len; _i++) {
                file = files[_i];
                file.xhr = xhr;
            }
            method = resolveOption(this.options.method, files);
            url = resolveOption(this.options.url, files);
            xhr.open(method, url, true);
            xhr.withCredentials = !!this.options.withCredentials;
            response = null;
            handleError = (function (_this) {
                return function () {
                    var _j, _len1, _results;
                    _results = [];
                    for (_j = 0, _len1 = files.length; _j < _len1; _j++) {
                        file = files[_j];
                        _results.push(_this._errorProcessing(files, response || _this.options.dictResponseError.replace("{{statusCode}}", xhr.status), xhr));
                    }
                    return _results;
                };
            })(this);
            updateProgress = (function (_this) {
                return function (e) {
                    var allFilesFinished, progress, _j, _k, _l, _len1, _len2, _len3, _results;
                    if (e != null) {
                        progress = 100 * e.loaded / e.total;
                        for (_j = 0, _len1 = files.length; _j < _len1; _j++) {
                            file = files[_j];
                            file.upload = {
                                progress: progress,
                                total: e.total,
                                bytesSent: e.loaded
                            };
                        }
                    } else {
                        allFilesFinished = true;
                        progress = 100;
                        for (_k = 0, _len2 = files.length; _k < _len2; _k++) {
                            file = files[_k];
                            if (!(file.upload.progress === 100 && file.upload.bytesSent === file.upload.total)) {
                                allFilesFinished = false;
                            }
                            file.upload.progress = progress;
                            file.upload.bytesSent = file.upload.total;
                        }
                        if (allFilesFinished) {
                            return;
                        }
                    }
                    _results = [];
                    for (_l = 0, _len3 = files.length; _l < _len3; _l++) {
                        file = files[_l];
                        _results.push(_this.emit("uploadprogress", file, progress, file.upload.bytesSent));
                    }
                    return _results;
                };
            })(this);
            xhr.onload = (function (_this) {
                return function (e) {
                    var _ref;
                    if (files[0].status === Dropzone.CANCELED) {
                        return;
                    }
                    if (xhr.readyState !== 4) {
                        return;
                    }
                    response = xhr.responseText;
                    if (xhr.getResponseHeader("content-type") && ~xhr.getResponseHeader("content-type").indexOf("application/json")) {
                        try {
                            response = JSON.parse(response);
                        } catch (_error) {
                            e = _error;
                            response = "Invalid JSON response from server.";
                        }
                    }
                    updateProgress();
                    if (!((200 <= (_ref = xhr.status) && _ref < 300))) {
                        return handleError();
                    } else {
                        return _this._finished(files, response, e);
                    }
                };
            })(this);
            xhr.onerror = (function (_this) {
                return function () {
                    if (files[0].status === Dropzone.CANCELED) {
                        return;
                    }
                    return handleError();
                };
            })(this);
            progressObj = (_ref = xhr.upload) != null ? _ref : xhr;
            progressObj.onprogress = updateProgress;
            headers = {
                "Accept": "application/json",
                "Cache-Control": "no-cache",
                "X-Requested-With": "XMLHttpRequest"
            };
            if (this.options.headers) {
                extend(headers, this.options.headers);
            }
            for (headerName in headers) {
                headerValue = headers[headerName];
                if (headerValue) {
                    xhr.setRequestHeader(headerName, headerValue);
                }
            }
            formData = new FormData();
            if (this.options.params) {
                _ref1 = this.options.params;
                for (key in _ref1) {
                    value = _ref1[key];
                    formData.append(key, value);
                }
            }
            for (_j = 0, _len1 = files.length; _j < _len1; _j++) {
                file = files[_j];
                this.emit("sending", file, xhr, formData);
            }
            if (this.options.uploadMultiple) {
                this.emit("sendingmultiple", files, xhr, formData);
            }
            if (this.element.tagName === "FORM") {
                _ref2 = this.element.querySelectorAll("input, textarea, select, button");
                for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
                    input = _ref2[_k];
                    inputName = input.getAttribute("name");
                    inputType = input.getAttribute("type");
                    if (input.tagName === "SELECT" && input.hasAttribute("multiple")) {
                        _ref3 = input.options;
                        for (_l = 0, _len3 = _ref3.length; _l < _len3; _l++) {
                            option = _ref3[_l];
                            if (option.selected) {
                                formData.append(inputName, option.value);
                            }
                        }
                    } else if (!inputType || ((_ref4 = inputType.toLowerCase()) !== "checkbox" && _ref4 !== "radio") || input.checked) {
                        formData.append(inputName, input.value);
                    }
                }
            }
            for (i = _m = 0, _ref5 = files.length - 1; 0 <= _ref5 ? _m <= _ref5 : _m >= _ref5; i = 0 <= _ref5 ? ++_m : --_m) {
                formData.append(this._getParamName(i), files[i], files[i].name);
            }
            return this.submitRequest(xhr, formData, files);
        };

        Dropzone.prototype.submitRequest = function (xhr, formData, files) {
            return xhr.send(formData);
        };

        Dropzone.prototype._finished = function (files, responseText, e) {
            var file, _i, _len;
            for (_i = 0, _len = files.length; _i < _len; _i++) {
                file = files[_i];
                file.status = Dropzone.SUCCESS;
                this.emit("success", file, responseText, e);
                this.emit("complete", file);
            }
            if (this.options.uploadMultiple) {
                this.emit("successmultiple", files, responseText, e);
                this.emit("completemultiple", files);
            }
            if (this.options.autoProcessQueue) {
                return this.processQueue();
            }
        };

        Dropzone.prototype._errorProcessing = function (files, message, xhr) {
            var file, _i, _len;
            for (_i = 0, _len = files.length; _i < _len; _i++) {
                file = files[_i];
                file.status = Dropzone.ERROR;
                this.emit("error", file, message, xhr);
                this.emit("complete", file);
            }
            if (this.options.uploadMultiple) {
                this.emit("errormultiple", files, message, xhr);
                this.emit("completemultiple", files);
            }
            if (this.options.autoProcessQueue) {
                return this.processQueue();
            }
        };

        return Dropzone;

    })(Emitter);

    Dropzone.version = "4.1.0";

    Dropzone.options = {};

    Dropzone.optionsForElement = function (element) {
        if (element.getAttribute("id")) {
            return Dropzone.options[camelize(element.getAttribute("id"))];
        } else {
            return void 0;
        }
    };

    Dropzone.instances = [];

    Dropzone.forElement = function (element) {
        if (typeof element === "string") {
            element = document.querySelector(element);
        }
        if ((element != null ? element.dropzone : void 0) == null) {
            throw new Error("No Dropzone found for given element. This is probably because you're trying to access it before Dropzone had the time to initialize. Use the `init` option to setup any additional observers on your Dropzone.");
        }
        return element.dropzone;
    };

    Dropzone.autoDiscover = true;

    Dropzone.discover = function () {
        var checkElements, dropzone, dropzones, _i, _len, _results;
        if (document.querySelectorAll) {
            dropzones = document.querySelectorAll(".dropzone");
        } else {
            dropzones = [];
            checkElements = function (elements) {
                var el, _i, _len, _results;
                _results = [];
                for (_i = 0, _len = elements.length; _i < _len; _i++) {
                    el = elements[_i];
                    if (/(^| )dropzone($| )/.test(el.className)) {
                        _results.push(dropzones.push(el));
                    } else {
                        _results.push(void 0);
                    }
                }
                return _results;
            };
            checkElements(document.getElementsByTagName("div"));
            checkElements(document.getElementsByTagName("form"));
        }
        _results = [];
        for (_i = 0, _len = dropzones.length; _i < _len; _i++) {
            dropzone = dropzones[_i];
            if (Dropzone.optionsForElement(dropzone) !== false) {
                _results.push(new Dropzone(dropzone));
            } else {
                _results.push(void 0);
            }
        }
        return _results;
    };

    Dropzone.blacklistedBrowsers = [/opera.*Macintosh.*version\/12/i];

    Dropzone.isBrowserSupported = function () {
        var capableBrowser, regex, _i, _len, _ref;
        capableBrowser = true;
        if (window.File && window.FileReader && window.FileList && window.Blob && window.FormData && document.querySelector) {
            if (!("classList" in document.createElement("a"))) {
                capableBrowser = false;
            } else {
                _ref = Dropzone.blacklistedBrowsers;
                for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                    regex = _ref[_i];
                    if (regex.test(navigator.userAgent)) {
                        capableBrowser = false;
                        continue;
                    }
                }
            }
        } else {
            capableBrowser = false;
        }
        return capableBrowser;
    };

    without = function (list, rejectedItem) {
        var item, _i, _len, _results;
        _results = [];
        for (_i = 0, _len = list.length; _i < _len; _i++) {
            item = list[_i];
            if (item !== rejectedItem) {
                _results.push(item);
            }
        }
        return _results;
    };

    camelize = function (str) {
        return str.replace(/[\-_](\w)/g, function (match) {
            return match.charAt(1).toUpperCase();
        });
    };

    Dropzone.createElement = function (string) {
        var div;
        div = document.createElement("div");
        div.innerHTML = string;
        return div.childNodes[0];
    };

    Dropzone.elementInside = function (element, container) {
        if (element === container) {
            return true;
        }
        while (element = element.parentNode) {
            if (element === container) {
                return true;
            }
        }
        return false;
    };

    Dropzone.getElement = function (el, name) {
        var element;
        if (typeof el === "string") {
            element = document.querySelector(el);
        } else if (el.nodeType != null) {
            element = el;
        }
        if (element == null) {
            throw new Error("Invalid `" + name + "` option provided. Please provide a CSS selector or a plain HTML element.");
        }
        return element;
    };

    Dropzone.getElements = function (els, name) {
        var e, el, elements, _i, _j, _len, _len1, _ref;
        if (els instanceof Array) {
            elements = [];
            try {
                for (_i = 0, _len = els.length; _i < _len; _i++) {
                    el = els[_i];
                    elements.push(this.getElement(el, name));
                }
            } catch (_error) {
                e = _error;
                elements = null;
            }
        } else if (typeof els === "string") {
            elements = [];
            _ref = document.querySelectorAll(els);
            for (_j = 0, _len1 = _ref.length; _j < _len1; _j++) {
                el = _ref[_j];
                elements.push(el);
            }
        } else if (els.nodeType != null) {
            elements = [els];
        }
        if (!((elements != null) && elements.length)) {
            throw new Error("Invalid `" + name + "` option provided. Please provide a CSS selector, a plain HTML element or a list of those.");
        }
        return elements;
    };

    Dropzone.confirm = function (question, accepted, rejected) {
        if (window.confirm(question)) {
            return accepted();
        } else if (rejected != null) {
            return rejected();
        }
    };

    Dropzone.isValidFile = function (file, acceptedFiles) {
        var baseMimeType, mimeType, validType, _i, _len;
        if (!acceptedFiles) {
            return true;
        }
        acceptedFiles = acceptedFiles.split(",");
        mimeType = file.type;
        baseMimeType = mimeType.replace(/\/.*$/, "");
        for (_i = 0, _len = acceptedFiles.length; _i < _len; _i++) {
            validType = acceptedFiles[_i];
            validType = validType.trim();
            if (validType.charAt(0) === ".") {
                if (file.name.toLowerCase().indexOf(validType.toLowerCase(), file.name.length - validType.length) !== -1) {
                    return true;
                }
            } else if (/\/\*$/.test(validType)) {
                if (baseMimeType === validType.replace(/\/.*$/, "")) {
                    return true;
                }
            } else {
                if (mimeType === validType) {
                    return true;
                }
            }
        }
        return false;
    };

    if (typeof jQuery !== "undefined" && jQuery !== null) {
        jQuery.fn.dropzone = function (options) {
            return this.each(function () {
                return new Dropzone(this, options);
            });
        };
    }

    if (typeof module !== "undefined" && module !== null) {
        module.exports = Dropzone;
    } else {
        window.Dropzone = Dropzone;
    }

    Dropzone.ADDED = "added";

    Dropzone.QUEUED = "queued";

    Dropzone.ACCEPTED = Dropzone.QUEUED;

    Dropzone.UPLOADING = "uploading";

    Dropzone.PROCESSING = Dropzone.UPLOADING;

    Dropzone.CANCELED = "canceled";

    Dropzone.ERROR = "error";

    Dropzone.SUCCESS = "success";


    /*
  
  Bugfix for iOS 6 and 7
  Source: http://stackoverflow.com/questions/11929099/html5-canvas-drawimage-ratio-bug-ios
  based on the work of https://github.com/stomita/ios-imagefile-megapixel
   */

    detectVerticalSquash = function (img) {
        var alpha, canvas, ctx, data, ey, ih, iw, py, ratio, sy;
        iw = img.naturalWidth;
        ih = img.naturalHeight;
        canvas = document.createElement("canvas");
        canvas.width = 1;
        canvas.height = ih;
        ctx = canvas.getContext("2d");
        ctx.drawImage(img, 0, 0);
        data = ctx.getImageData(0, 0, 1, ih).data;
        sy = 0;
        ey = ih;
        py = ih;
        while (py > sy) {
            alpha = data[(py - 1) * 4 + 3];
            if (alpha === 0) {
                ey = py;
            } else {
                sy = py;
            }
            py = (ey + sy) >> 1;
        }
        ratio = py / ih;
        if (ratio === 0) {
            return 1;
        } else {
            return ratio;
        }
    };

    drawImageIOSFix = function (ctx, img, sx, sy, sw, sh, dx, dy, dw, dh) {
        var vertSquashRatio;
        vertSquashRatio = detectVerticalSquash(img);
        return ctx.drawImage(img, sx, sy, sw, sh, dx, dy, dw, dh / vertSquashRatio);
    };


    /*
   * contentloaded.js
   *
   * Author: Diego Perini (diego.perini at gmail.com)
   * Summary: cross-browser wrapper for DOMContentLoaded
   * Updated: 20101020
   * License: MIT
   * Version: 1.2
   *
   * URL:
   * http://javascript.nwbox.com/ContentLoaded/
   * http://javascript.nwbox.com/ContentLoaded/MIT-LICENSE
   */

    contentLoaded = function (win, fn) {
        var add, doc, done, init, poll, pre, rem, root, top;
        done = false;
        top = true;
        doc = win.document;
        root = doc.documentElement;
        add = (doc.addEventListener ? "addEventListener" : "attachEvent");
        rem = (doc.addEventListener ? "removeEventListener" : "detachEvent");
        pre = (doc.addEventListener ? "" : "on");
        init = function (e) {
            if (e.type === "readystatechange" && doc.readyState !== "complete") {
                return;
            }
            (e.type === "load" ? win : doc)[rem](pre + e.type, init, false);
            if (!done && (done = true)) {
                return fn.call(win, e.type || e);
            }
        };
        poll = function () {
            var e;
            try {
                root.doScroll("left");
            } catch (_error) {
                e = _error;
                setTimeout(poll, 50);
                return;
            }
            return init("poll");
        };
        if (doc.readyState !== "complete") {
            if (doc.createEventObject && root.doScroll) {
                try {
                    top = !win.frameElement;
                } catch (_error) {
                }
                if (top) {
                    poll();
                }
            }
            doc[add](pre + "DOMContentLoaded", init, false);
            doc[add](pre + "readystatechange", init, false);
            return win[add](pre + "load", init, false);
        }
    };

    Dropzone._autoDiscoverFunction = function () {
        if (Dropzone.autoDiscover) {
            return Dropzone.discover();
        }
    };

    contentLoaded(window, Dropzone._autoDiscoverFunction);

}).call(this);
