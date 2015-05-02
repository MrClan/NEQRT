$().ready(function () {
    window.separator = "```";
    window.valSeparator = "******";
    window.noOfCommoditiesOnPage = 0;
    window.memberCount = 0;
    window.afterInsertCommodityTemplate = "<div class='row'><div class='six columns'><label for='c_count_```' style='text-transform:capitalize;' class='cname'>******</label></div><div class='six columns'><input type='hidden' name='c_```' value='------' /><input type='number' name='ccount_```' id='ccount_```' /></div></div>";

    jQuery('#DepartureOn').datetimepicker();
    jQuery('#ETA').datetimepicker();

    $('#addNewCommodity').click(function (e) {
        e.preventDefault();
        $(this).attr('disabled', 'disabled');
        console.log('adding new commodity');
        var comName = prompt("Commodity name:").trim();
        if (ValidateUniqueName($('.cname'), comName)) {
            $.post("/ajax/insertcommodity", { name: comName }, function (data, status, xhr) {
                $('#insertCommodity').remove();
                $('#c_name').remove();
                $('#addNewCommodity').removeAttr('disabled');
                if (data.success == true) {
                    window.noOfCommoditiesOnPage++;
                    $('#existingCommodities').append(window.afterInsertCommodityTemplate.split(window.separator).join(window.noOfCommoditiesOnPage).split("******").join(comName).split("------").join(data.id));
                }
                else {
                    alert(data.msg);
                }
            }, "json");
        }
        else {
            alert('Item already exists.');
            $('#c_name').focus();
            $('#addNewCommodity').removeAttr('disabled');
            return false;
        }
    });


    $('#addNewDestination').click(function (e) {
        e.preventDefault();
        $(this).attr('disabled', 'disabled');
        console.log('inserting new destination');
        console.log('adding new destination');
        //var noOfCommoditiesOnPage = FindNextCommodityId();
        var dName = prompt("Destination name:").trim();
        console.log(dName);
        if (ValidateUniqueName($('#Destination option'), dName)) {
            $.post("/ajax/insertdestination", { name: dName }, function (data, status, xhr) {
                $('#addNewDestination').removeAttr('disabled');
                if (data.success == true) {
                    $('#Destination').append("<option value='" + data.id + "'>" + dName + "</option>");
                    $('#Destination option[value=' + data.id + ']').attr('selected', 'true');
                }
                else {
                    alert(data.msg);
                }

            }, "json");
        }
        else {
            alert('Destination already exists.');
            $('#addNewDestination').removeAttr('disabled');
            return false;
        }
    });

    $('#addNewMember').click(function (e) {
        e.preventDefault();
        var memDetails = prompt("Member name,contact number:");
        if (memDetails.indexOf(",") == -1) {
            var mNo = "";
            var mName = "";
        }
        else {
            var mName = memDetails.split(",")[0].trim();
            var mNo = memDetails.split(",")[1].trim();
        }
        if (mName.trim().length == 0 || mNo.trim().length == 0) {
            return false;
        }
        else {
            window.memberCount++;
            $('#teamMembers').append("<div class='row'>" + window.memberCount + ". <input type='text' name='m_" + window.memberCount + "' value='" + mName + "'/> <input type='text' name='mn_" + window.memberCount + "' value='" + mNo + "' /></div>");
        }
    });


    function ValidateUniqueName(selector, name) {
        if (!name || name.length <= 3) {
            return false;
        }
        name = name.toLowerCase();
        var curVals = _.map(selector, function (c) {
            return $(c).text().toLowerCase();
        });
        return curVals.indexOf(name) == -1;
    }


    //$("#frmEntry").validate();

    // validate main form on keyup and submit
    $("#frmEntry").validate({
        rules: {
            TeamName: "required",
            memCount: "required",
            primaryContact: "required",
            Destination: "required",
            DepartureFrom: "required",
            DestinatureOn: "required",
            ETA: "required",
            //username: {
            //    required: true,
            //    minlength: 2
            //}
            //,password: {
            //    required: true,
            //    minlength: 5
            //},
            //confirm_password: {
            //    required: true,
            //    minlength: 5,
            //    equalTo: "#password"
            //},
            //email: {
            //    required: true,
            //    email: true
            //},
            //topic: {
            //    required: "#newsletter:checked",
            //    minlength: 2
            //},
            //agree: "required"
        },
        //errorLabelContainer: '#errors',
        messages: {
            TeamName: "Team name is required",
            memCount: "Please enter the no of members in your team"
            //,username: {
            //    required: "Please enter a username",
            //    minlength: "Your username must consist of at least 2 characters"
            //},
            //password: {
            //    required: "Please provide a password",
            //    minlength: "Your password must be at least 5 characters long"
            //},
            //confirm_password: {
            //    required: "Please provide a password",
            //    minlength: "Your password must be at least 5 characters long",
            //    equalTo: "Please enter the same password as above"
            //},
            //email: "Please enter a valid email address",
            //agree: "Please accept our policy"
        }
    });
});