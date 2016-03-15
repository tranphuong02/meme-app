﻿// setup ajax
$.ajaxSetup({
    cache: false
});

Array.prototype.remove = function () {
    var what, a = arguments, l = a.length, ax;
    while (l && this.length) {
        what = a[--l];
        while ((ax = this.indexOf(what)) !== -1) {
            this.splice(ax, 1);
        }
    }
    return this;
};

Array.prototype.getUnique = function () {
    var u = {}, a = [];
    for (var i = 0, l = this.length; i < l; ++i) {
        if (u.hasOwnProperty(this[i])) {
            continue;
        }
        a.push(this[i]);
        u[this[i]] = 1;
    }
    return a;
}

function maximumFileSize() {
    return {
        image: 4096000,
        text: 1024000
    }
}

function generateUIDNotMoreThan1million() {
    return ((Math.random()).toString(36));
}

function hashCode(str) { // java String#hashCode
    var hash = 0;
    for (var i = 0; i < str.length; i++) {
        hash = str.charCodeAt(i) + ((hash << 5) - hash);
    }
    return hash;
}

function intToRGB(i) {
    var c = (i & 0x00FFFFFF)
        .toString(16)
        .toUpperCase();

    return "#" + "00000".substring(0, 6 - c.length) + c;
}

function commonVariables() {
    /// <summary>
    /// Define all the common for the front-end.
    /// </summary>
    /// <returns type=""></returns>
    return {
        // Define the image type
        imageType: ["png", "jpg", "gif", "jpeg", "ico"],
        imageRule: "png|jpg|gif|jpeg|ico",

        // plain text
        plainTextRule: "txt",
        plainTextAccept: "text/plain",
        // All
        allValue: -1,
        allText: "All",
        // Sweet alert
        autoCloseTimer: 2000
    };
}

function httpMethod() {
    return {
        POST: "POST",
        GET: "GET",
        PUT: "PUT",
        DELETE: "DELETE"
    };
}

function addListener_Keyboard_Enter($elementSource, $elementDestination, action) {
    /// <summary>
    ///     Add listener when element source press enter make element destination fire a action
    /// </summary>
    /// <param name="$elementSource" type="type">jquery object</param>
    /// <param name="$elementDestination" type="type">jquery object</param>
    /// <param name="action" type="type">action: "click", "dbclick" and so on</param>
    $elementSource.keydown(function (e) {
        if (e.which === 13) {
            $elementDestination.trigger(action);
        }
    });
}

function addListener_Keyboard_Ctrl_Enter($elementSource, $elementDestination, action) {
    /// <summary>
    ///     Add listener when element source press ctrl + enter make element destination fire a action
    /// </summary>
    /// <param name="$elementSource" type="type">jquery object</param>
    /// <param name="$elementDestination" type="type">jquery object</param>
    /// <param name="action" type="type">action: "click", "dbclick" and so on</param>
    $elementSource.keydown(function (e) {
        if (e.ctrlKey && e.keyCode == 13) {
            $elementDestination.trigger(action);
        }
    });
}

function datatableToolkitUrl() {
    return {
        sSwfPath: "/Scripts/plugins/dataTables/swf/copy_csv_xls_pdf.swf"
    }
}

function dateTimeFormatConstant() {
    return {
        getFullDateTimeFormat: "dd/mm/yyyy hh:MM:ss TT",
        dateTimeFormat: "dd/mm/yyyy"
    }
}

function replaceAll(str, find, replace) {
    /// <summary>
    ///  replay all
    /// </summary>
    /// <param name="str" type="type">source</param>
    /// <param name="find" type="type">string search</param>
    /// <param name="replace" type="type">string replace</param>
    /// <returns type=""></returns>
    return str.replace(new RegExp(find, 'g'), replace);
}

function getNumberOnly(source) {
    /// <summary>
    ///     Get number from string
    /// </summary>
    /// <param name="source" type="type"></param>
    return parseInt(source.replace(/\D/g, ''));
}

function getFullTime(strTime) {
    /// <summary>
    /// Get full time: dd/mm/yyyy hh:mm AM/PM
    /// </summary>
    /// <param name="strTime"></param>
    /// <returns type=""></returns>
    var dateTime = new Date(strTime);
    var timeOfSet = dateTime.getTimezoneOffset();
    var utcdateTime = new Date(dateTime.getTime() + (timeOfSet * 60 * 1000));

    var date = utcdateTime.getDate();
    var month = utcdateTime.getMonth() + 1;
    var year = utcdateTime.getFullYear();

    var hours = utcdateTime.getHours();
    var minutes = utcdateTime.getMinutes();
    var seconds = utcdateTime.getSeconds();

    return date + "/" + month + "/" + year + " " + hours + ":" + minutes + ":" + seconds;
}

function getDate(strTime) {
    /// <summary>
    /// Get full time: dd/mm/yyyy
    /// </summary>
    /// <param name="strTime"></param>
    /// <returns type=""></returns>
    var dateTime = new Date(strTime);

    var timeOfSet = dateTime.getTimezoneOffset();
    var utcdateTime = new Date(dateTime.getTime() + (timeOfSet * 60 * 1000));

    var date = utcdateTime.getDate();
    var month = utcdateTime.getMonth() + 1;
    var year = utcdateTime.getFullYear();

    return date + "/" + month + "/" + year;
}

String.prototype.preventInjection = function preventInjection() {
    return this.replace(/</g, "&lt;").replace(/>/g, "&gt;");
};

String.prototype.genUrl = function changeToSlug() {
    var title, slug;
    title = this;

    slug = title.toLowerCase();

    slug = slug.replace(/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi, 'a');
    slug = slug.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'e');
    slug = slug.replace(/i|í|ì|ỉ|ĩ|ị/gi, 'i');
    slug = slug.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'o');
    slug = slug.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'u');
    slug = slug.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'y');
    slug = slug.replace(/đ/gi, 'd');

    slug = slug.replace(/\`|\~|\!|\@|\#|\||\$|\%|\^|\&|\*|\(|\)|\+|\=|\,|\.|\/|\?|\>|\<|\'|\"|\:|\;|_/gi, '');

    slug = slug.replace(/ /gi, "-");

    slug = slug.replace(/\-\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-/gi, '-');
    slug = slug.replace(/\-\-/gi, '-');

    slug = '@' + slug + '@';
    slug = slug.replace(/\@\-|\-\@|\@/gi, '');

    return slug;
}

function isValidImage(file) {
    /// <summary>
    /// Check the input is a file
    /// </summary>
    /// <returns type=""></returns>
    var extension = $.trim(file.name.split('.').pop().toLocaleLowerCase());
    return $.inArray(extension, commonVariables().imageType) === 0;
}

function subString(text, length) {
    text += "";

    if (text.length > length) {
        text = text.substr(0, length)+"...";
    }

    return text.preventInjection();
}

function getTimeFromUtcTime() {
    var date = new Date();
    var hours = date.getUTCHours();
    var minutes = date.getUTCMinutes();

    return hours + ":" + minutes;
}

function getLocalDate($element,date, time) {
    try {
        var day = date.split('/')[0];
        var month = date.split('/')[1];
        var year = date.split('/')[2];

        var hours = time.split(':')[0];
        var minutes = time.split(':')[1];

        var newDate = convertUTCDateToLocalDate(new Date(year, month, day, hours, minutes, 0));
        $element.text(getFullDateStringFromDate(newDate));
    } catch (ex) {
        $element.text(getFullDateStringFromDate(new Date()));
    } 
}

function convertUTCDateToLocalDate(date) {
    var newDate = new Date(date.getTime() + date.getTimezoneOffset() * 60 * 1000);

    var offset = date.getTimezoneOffset() / 60;
    var hours = date.getHours();

    newDate.setHours(hours - offset);

    return newDate;
}

function getFullDateStringFromDate(date) {
    return date.getDate() + "/" + (date.getMonth()) + "/" + date.getFullYear() + " " + date.getHours() + ":" + date.getMinutes();
}

function chooseAllItemInTheSelectBox($element) {
    var selectedValue = parseInt($element.val());
    if (selectedValue === commonVariables().allValue) {
        $element.find('option').prop("selected", true);
        $element.find('option[value="' + commonVariables().allValue + '"]').prop("selected", false).trigger("chosen:updated");
    }
}

function chooseAllItemInTheOutletSelectBox($element) {
    var selectedValue = parseInt($element.val());
    if (selectedValue === commonVariables().allValue) {
        $element.find('option').prop("selected", false).attr("disabled", "disabled");
        $element.find('option[value="' + commonVariables().allValue + '"]').prop("selected", true).removeAttr("disabled").trigger("chosen:updated");
    }
}

function removeItemInTheOutletSelectBox($element, value) {
    var selectedValue = parseInt(value);
    if (selectedValue === commonVariables().allValue) {
        $element.find('option').prop("selected", false).removeAttr("disabled").trigger("chosen:updated");
    }
}

function chooseOptionAllForOutlet($element) {
    $element.find('option').prop("selected", false).attr("disabled", "disabled");
    $element.find('option[value="' + commonVariables().allValue + '"]').prop("selected", true).removeAttr("disabled").trigger("chosen:updated");
}

function renderAllOption($element) {
    var $all = $('<option value="' + commonVariables().allValue + '">' + commonVariables().allText + '</option>');
    $element.append($all);
}

function renderAllOptionOutlet($element) {
    var $all = $('<option value="' + commonVariables().allValue + '">' + commonVariables().allText + ' (Left Blank)</option>');
    $element.append($all);
}

function renderTooltopDateTime(data) {
    
    var fullDateTime = new Date(parseInt(data.substr(6))).format(dateTimeFormatConstant().getFullDateTimeFormat);
    var dateTime = new Date(parseInt(data.substr(6))).format(dateTimeFormatConstant().dateTimeFormat);
    return '<span><span data-toggle="tooltip" data-placement="right" title="" data-original-title="' + fullDateTime + '">' + dateTime + '</span></span>';
}

function selectRowDatatableEffect($dataTable, $oTable) {
    $dataTable.find("tbody tr").live("click", function (e) {
        e.stopImmediatePropagation();
        if ($(this).hasClass("selected-row")) {
        } else {
            $oTable.$("tr.selected-row").removeClass("selected-row");
            $(this).addClass("selected-row");
        }
    });
}

function reloadDatatable($oTable) {
    $oTable.ajax.reload(null, false);
}

function save() {
    var $btnCancel = $('#btnCancel');
    var $form = $('#form');

    if ($form.valid()) {
        var ladda = Ladda.create(document.querySelector("#btnSave"));
        $btnCancel.css('pointer-events', 'none').css('cursor', 'default').css("opacity", "0.6");
        ladda.start();
        $form.submit();
    }
}