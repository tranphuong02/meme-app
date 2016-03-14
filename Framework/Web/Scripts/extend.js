/**
 * Forward port jQuery.live()
 * Wrapper for newer jQuery.on()
 * Uses optimized selector context
 * Only add if live() not already existing.
*/
if (typeof jQuery.fn.live == 'undefined' || !(jQuery.isFunction(jQuery.fn.live))) {
    jQuery.fn.extend({
        live: function (event, callback) {
            if (this.selector) {
                jQuery(document).on(event, this.selector, callback);
            }
        }
    });
}

toastr.options = {
    "closeButton": true,
    "debug": false,
    "progressBar": true,
    "preventDuplicates": false,
    "positionClass": "toast-top-right",
    "onclick": null,
    "showDuration": "500",
    "hideDuration": "1000",
    "timeOut": "7000",
    "extendedTimeOut": "1000",
    "showEasing": "easeOutBounce",
    "hideEasing": "easeInBack",
    "showMethod": "slideDown",
    "hideMethod": "slideUp"
}

$("#top-search").keyup(function (e) {
    // escape
    if (e.keyCode == 27) {
        $("#top-search").val('');
    }

    var searchString = $("#top-search").val().trim();
    var ignore = [":", ";", ".", "-", "/", "\\", "#", "@", "$", "%", "^", "&", "*", "(", ")", "=", "+"];

    if ($.inArray(searchString, ignore) > -1) {
        return;
    }

    MakeHighlight(searchString);
});

function ToggleContentLoading(isShow) {
    if (isShow) {
        $("#content-loading").removeClass("hidden");
    } else {
        $("#content-loading").addClass("hidden");
    }
}

function MakeHighlight(text) {
    var myHighlight = new Highlight("body");
    myHighlight.setMatchType("open");
    myHighlight.apply(text);
}

function Highlight(id, tag) {
    var targetNode = document.getElementById(id) || document.body;
    var hiliteTag = tag || "EM";
    var skipTags = new RegExp("^(?:" + hiliteTag + "|SCRIPT|FORM)$");
    var colors = ["#ff6", "#a0ffff", "#9f9", "#f99", "#f6f"];
    var wordColor = [];
    var colorIdx = 0;
    var matchRegex = "";
    var openLeft = false;
    var openRight = false;

    this.setMatchType = function (type) {
        switch (type) {
            case "left":
                this.openLeft = false;
                this.openRight = true;
                break;
            case "right":
                this.openLeft = true;
                this.openRight = false;
                break;
            case "open":
                this.openLeft = this.openRight = true;
                break;
            default:
                this.openLeft = this.openRight = false;
        }
    };

    function addAccents(input) {
        var retval = input;
        retval = retval.replace(/([ao])e/ig, "$1");
        retval = retval.replace(/\\u00E[024]/ig, "a");
        retval = retval.replace(/\\u00E[89AB]/ig, "e");
        retval = retval.replace(/\\u00E[EF]/ig, "i");
        retval = retval.replace(/\\u00F[46]/ig, "o");
        retval = retval.replace(/\\u00F[9BC]/ig, "u");
        retval = retval.replace(/\\u00FF/ig, "y");
        retval = retval.replace(/\\u00DF/ig, "s");
        retval = retval.replace(/a/ig, "([aàâä]|ae)");
        retval = retval.replace(/e/ig, "[eèéêë]");
        retval = retval.replace(/i/ig, "[iîï]");
        retval = retval.replace(/o/ig, "([oôö]|oe)");
        retval = retval.replace(/u/ig, "[uùûü]");
        retval = retval.replace(/y/ig, "[yÿ]");
        retval = retval.replace(/s/ig, "(ss|[sß])");
        return retval;
    }

    this.setRegex = function (input) {
        input = input.replace(/\\([^u]|$)/g, "$1");
        input = input.replace(/[^\w\\\s']+/g, "").replace(/\s+/g, "|");
        input = addAccents(input);
        var re = "(" + input + ")";
        if (!this.openLeft) re = "(?:^|[\\b\\s])" + re;
        if (!this.openRight) re = re + "(?:[\\b\\s]|$)";
        matchRegex = new RegExp(re, "i");
    };

    this.getRegex = function () {
        var retval = matchRegex.toString();
        retval = retval.replace(/(^\/|\(\?:[^\)]+\)|\/i$)/g, "");
        return retval;
    };

    // recursively apply word highlighting
    this.hiliteWords = function (node) {
        if (node === undefined || !node) return;
        if (!matchRegex) return;
        if (skipTags.test(node.nodeName)) return;

        if (node.hasChildNodes()) {
            for (var i = 0; i < node.childNodes.length; i++)
                this.hiliteWords(node.childNodes[i]);
        }
        if (node.nodeType == 3) { // NODE_TEXT
            if ((nv = node.nodeValue) && (regs = matchRegex.exec(nv))) {
                if (!wordColor[regs[1].toLowerCase()]) {
                    wordColor[regs[1].toLowerCase()] = colors[colorIdx++ % colors.length];
                }

                var match = document.createElement(hiliteTag);
                match.appendChild(document.createTextNode(regs[1]));
                match.style.backgroundColor = wordColor[regs[1].toLowerCase()];
                match.style.fontStyle = "inherit";
                match.style.color = "#000";

                var after;
                if (regs[0].match(/^\s/)) { // in case of leading whitespace
                    after = node.splitText(regs.index + 1);
                } else {
                    after = node.splitText(regs.index);
                }
                after.nodeValue = after.nodeValue.substring(regs[1].length);
                node.parentNode.insertBefore(match, after);
            }
        };
    };

    // remove highlighting
    this.remove = function () {
        var arr = document.getElementsByTagName(hiliteTag);
        while (arr.length && (el = arr[0])) {
            var parent = el.parentNode;
            parent.replaceChild(el.firstChild, el);
            parent.normalize();
        }
    };

    // start highlighting at target node
    this.apply = function (input) {
        this.remove();
        if (input === undefined || !(input = input.replace(/(^\s+|\s+$)/g, ""))) return;
        input = convertCharStr2JEsc(input);
        this.setRegex(input);
        this.hiliteWords(targetNode);
    };

    // added by Yanosh Kunsh to include utf-8 string comparison
    function dec2Hex4(textString) {
        var hexequiv = new Array("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F");
        return hexequiv[(textString >> 12) & 0xF] + hexequiv[(textString >> 8) & 0xF] + hexequiv[(textString >> 4) & 0xF] + hexequiv[textString & 0xF];
    }

    function convertCharStr2JEsc(str, cstyle) {
        // Converts a string of characters to JavaScript escapes
        // str: sequence of Unicode characters
        var highsurrogate = 0;
        var suppCP;
        var pad;
        var n = 0;
        var outputString = '';
        for (var i = 0; i < str.length; i++) {
            var cc = str.charCodeAt(i);
            if (cc < 0 || cc > 0xFFFF) {
                outputString += '!Error in convertCharStr2UTF16: unexpected charCodeAt result, cc=' + cc + '!';
            }
            if (highsurrogate != 0) { // this is a supp char, and cc contains the low surrogate
                if (0xDC00 <= cc && cc <= 0xDFFF) {
                    suppCP = 0x10000 + ((highsurrogate - 0xD800) << 10) + (cc - 0xDC00);
                    if (cstyle) {
                        pad = suppCP.toString(16);
                        while (pad.length < 8) {
                            pad = '0' + pad;
                        }
                        outputString += '\\U' + pad;
                    } else {
                        suppCP -= 0x10000;
                        outputString += '\\u' + dec2Hex4(0xD800 | (suppCP >> 10)) + '\\u' + dec2Hex4(0xDC00 | (suppCP & 0x3FF));
                    }
                    highsurrogate = 0;
                    continue;
                } else {
                    outputString += 'Error in convertCharStr2UTF16: low surrogate expected, cc=' + cc + '!';
                    highsurrogate = 0;
                }
            }
            if (0xD800 <= cc && cc <= 0xDBFF) { // start of supplementary character
                highsurrogate = cc;
            } else { // this is a BMP character
                switch (cc) {
                    case 0:
                        outputString += '\\0';
                        break;
                    case 8:
                        outputString += '\\b';
                        break;
                    case 9:
                        outputString += '\\t';
                        break;
                    case 10:
                        outputString += '\\n';
                        break;
                    case 13:
                        outputString += '\\r';
                        break;
                    case 11:
                        outputString += '\\v';
                        break;
                    case 12:
                        outputString += '\\f';
                        break;
                    case 34:
                        outputString += '\\\"';
                        break;
                    case 39:
                        outputString += '\\\'';
                        break;
                    case 92:
                        outputString += '\\\\';
                        break;
                    default:
                        if (cc > 0x1f && cc < 0x7F) {
                            outputString += String.fromCharCode(cc);
                        } else {
                            pad = cc.toString(16).toUpperCase();
                            while (pad.length < 4) {
                                pad = '0' + pad;
                            }
                            outputString += '\\u' + pad;
                        }
                }
            }
        }
        return outputString;
    }
}

function LoadingEnable() {
    $("#page-loading").removeClass("hidden");
}

function LoadingDisable() {
    $("#page-loading").addClass("hidden");
}

$(document).ready(function () {
    LoadingDisable();

    $(".menu-item").live('click', function () {
        LoadingEnable();
    });

    // Hidden status messages
    setTimeout(function () {
        $("div.alert-dismissable").fadeOut();
    }, 5000);
});