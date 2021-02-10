window.onload = function () {
    changeCodeEditor("empty");
}


jQuery.fn.dataTable.render.ellipsis = function (cutoff, wordbreak, escapeHtml) {
    var esc = function (t) {
        return t
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(/"/g, '&quot;');
    };
   
    return function (d, type, row) {
        // Order, search and type get the original data
        if (type !== 'display') {
            return d;
        }

        if (typeof d !== 'number' && typeof d !== 'string') {
            return d;
        }

        d = d.toString(); // cast numbers

        if (d.length <= cutoff) {
            return d;
        }

        var shortened = d.substr(0, cutoff - 1);

        // Find the last white space character in the string
        if (wordbreak) {
            shortened = shortened.replace(/\s([^\s]*)$/, '');
        }

        // Protect against uncontrolled HTML input
        if (escapeHtml) {
            shortened = esc(shortened);
        }

        return '<span class="ellipsis" title="' + esc(d) + '">' + shortened + '&#8230;</span>';
    };
};
tooltipAdd = function () {
    function addTooltip() {
        $(".sorting_disabled").each(function () {
            var thisTxt = $(this).text();
            var cloneEle = document.createElement("div");
            cloneEle = $(cloneEle);
            cloneEle.addClass("ele-clone");
            cloneEle.html(thisTxt);
            $("body").append(cloneEle);
            if ($(this).width() <= cloneEle.width()) {
                $(this).attr("title", thisTxt);
                $(this).tooltip();
            } else {
                $(this).removeAttr("title");
            }
            cloneEle.remove();
        });
    };
    addTooltip();
};

function populateSideBarStatic() {
    var liId = "databasesLi";
    var li = '<li id="' + liId + '"></li>';
    var a = '<a href="#databasesSubmenu" data-toggle="collapse" aria-expanded="false" data-parent="#insertAfter" class="dropdown-toggle sidebar-link titles neededFunction"></a>';
    var iAndSpan = '<i class="fa fa-database database-li-style"></i><span class="nav-label">Databases</span>';
    var ul = '<ul class="collapse sublist-one p-0" id="databasesSubmenu"></ul>';
    var databasesEntries = '<li id=databasesEntries></>';
    $('#insertAfter').append($(li).append($(a).append(iAndSpan)));
    $('#' + liId + '').append($(ul).append($(databasesEntries)));

    var liSyntax = '<li id="syntaxID"></li>';
    var aS = '<a href="#syntaxSubmenu" data-toggle="collapse" aria-expanded="false" data-parent="#insertAfter" class="dropdown-toggle sidebar-link titles neededFunction"></a>';
    var iAndSpanS = '<i class="fa fa-book book-li-style"></i><span class="nav-label">Syntax</span>';
    var ulS = '<ul class="collapse sublist-one p-0" id="syntaxSubmenu"></ul>';
    var syntaxEntries = '<li id=syntaxEntries></>';
    $('#insertAfter').append($(liSyntax).append($(aS).append(iAndSpanS)));
    $('#syntaxID').append($(ulS).append($(syntaxEntries)));
    populateSyntax();
}
function populateDatabase(i, nameOfDatabase) {
    var liId = "bd" + i + "FirstLi";
    var li = '<li id="bd' + i + 'FirstLi"></li>';
    var a = '<a href="#bd' + i + 'Submenu" data-toggle="collapse" aria-expanded="false" data-parent="#insertAfter" class="dropdown-toggle sidebar-link titles neededFunction" id="bd' + i + '"></a>';
    var iAndSpan = '<i class="fa fa-database database-li-style"></i><span class="nav-label">' + nameOfDatabase + '</span>';
    var ul = '<ul class="collapse sublist-one p-0" id="bd' + i + 'Submenu"></ul>';
    var liTables = '<li id=bd' + i + 'Tables></>';
    var indexTables = '<p class="nav-label open-li">Tables</p>';
    $('#databasesEntries').append($(li).append($(a).append(iAndSpan)));
    $('#' + liId + '').append($(ul).append($(liTables).append(indexTables)));
}
function populateTables(i, x, nameOfTable) {
    var liId = 'bd' + i + 'Tables';
    var a = '<a href="#bd' + i + 'table' + x + 'Submenu" data-toggle="collapse" aria-expanded="false" class="dropdown-toggle sidebar-link table-selector"></a>';
    var iHtml = '<i class="fa fa-table database-li-style"></i>';
    var span = '<span class="nav-label context-menu-one font-size-14px" id="bd' + i + 'table' + x + '">' + nameOfTable + '</span>';
    $('#' + liId + '').append($(a).append($(iHtml)).append($(span)));
    var ul = '<ul class="collapse sublist-two" id="bd' + i + 'table' + x + 'Submenu"></ul>';
    var li = '<li id=bd' + i + 'Table' + x + 'Columns></li>';
    var liIdSecond = 'bd' + i + 'Table' + x + 'Columns';
    var aSecond = '<a href="#bd' + i + 'column' + x + 'Submenu" data-toggle="collapse" aria-expanded="false" class="dropdown-toggle sidebar-link"></a>';
    var iHtmlSecond = '<i class="fa fa-columns database-li-style"></i><span class="nav-label font-size-14px">Columns</span>';
    var ulSecond = '<ul class="collapse sublist-three" id="bd' + i + 'column' + x + 'Submenu"></ul>';
    $('#' + liId + '').append($(ul).append($(li).append($(aSecond).append($(iHtmlSecond)))));
    $('#' + liIdSecond + '').append($(ulSecond));
}
function populateColumns(i, x, z, tittle, type) {
    var ulId = 'bd' + i + 'column' + x + 'Submenu';

    var li = '<li></li>';
    var a = '<a href="javascript:void(0)" class="color-black p-0 font-size-12px" data-toggle="tooltip" data-placement="top" title="' + tittle + '" data-original-title="' + tittle + '"></a>';
    if (z == 0) {
        var aContent = '<div data-column-type="' + type + '"><i class="fa fa-key margin-left-2-2rem pr-1"></i>' + tittle + '</div>';
    } else {
        var aContent = '<div data-column-type="' + type + '" style="margin-left:-20px">' + tittle + '</div>';
    }
    $('#' + ulId + '').append($(li).append($(a).append($(aContent))));
}

var syntaxTitles = ["CREATE DATABASE", "CREATE TABLE", "complex_name", "column_definition", "USE DATABASE", "DROP TABLE", "DROP DATABASE", "ALTER TABLE", "INSERT RECORD", "UPDATE RECORD", "DELETE RECORD", "SELECT RECORD", "expression", "IF STATEMENT"].sort();
function populateSyntax() {
    for (var i = 0; i < syntaxTitles.length; i++) {
        var li = '<li></li>';
        var a = '<a href="javascript:void(0)" onclick="getSyntaxData(\'' + syntaxTitles[i] + '\');" class="color-black p-0 font-size-12px" data-toggle="tooltip" data-placement="top" title="' + syntaxTitles[i] + '" data-original-title="' + syntaxTitles[i] + '"></a>';
        var aContent = '<div class="syntaxEntries" style="margin-left:20px; padding-bottom:5px;">' + syntaxTitles[i] + '</div>';
        $('#syntaxSubmenu').append($(li).append($(a).append($(aContent))));
    }
}

function getSyntaxData(title) {
    $.ajax({
        type: "GET",
        url: `/sandbox/Syntax`,
        success: function (result) {
            console.log(result)
            if (result == null) {
                alert('Error loading syntax.');
            } else {
                var myNode = document.getElementById("dataTablesZone");
                while (myNode.firstChild) {
                    myNode.removeChild(myNode.firstChild);
                }

                myNode.className = "syntaxBackground";

                var data = result[title];

                var titleDiv = '<div class="syntaxTitleDiv"> \
                        <h2 class="syntaxTitle">' + title + '</h2> \
                    </div> \
                </div>';

                var schemaDiv = '<div class="syntaxBox"> \
                            <div class="syntaxDataDiv"> \
                                <h5>Schema</h5> \
                            </div> \
                            <div style="padding-top: 5px;"> \
                                <pre role="presentation"><div id="' + title + "Schema" + '" style="padding-right: 0.1px;"></div></pre> \
                            </div> \
                        </div>';

                var exampleDiv = '<div class="syntaxBox"> \
                    <div class="syntaxDataDiv"> \
                        <h5>Example</h5> \
                    </div>';

                exampleDiv += '<div style="padding-top: 5px;"> \
                    <pre role="presentation"><div id="' + title + "Example" + '" role="presentation" style="padding-right: 0.1px;"></div></pre> \
                </div>';

                exampleDiv += '</div>';

                var descriptionDiv = '<div class="syntaxBox"> \
                    <div class="syntaxDataDiv"> \
                        <h5>Description</h5> \
                    </div>';

                descriptionDiv += '<div style="padding-bottom: 25px";> \
                    <pre role="presentation"><div id="' + title + "Description" + '" role="presentation" style="padding-right: 0.1px;"></div></pre>' + data.Description + '</div>';

                descriptionDiv += '</div>';



                $("#dataTablesZone").append($(titleDiv)).append($(schemaDiv)).append($(descriptionDiv)).append($(exampleDiv));

                createReadOnlyEditor(title + "Schema", data.Schema);
                createReadOnlyEditor(title + "Example", data.Example);
            }
            console.log(result[title]);
        },
        error: function (result) {
            console.log(result);
            alert('Error loading syntax.');
        }
    });
}
function createReadOnlyEditor(elementId, value) {
    var editor = CodeMirror(document.getElementById(elementId), {
        value: value,
        mode: "sql",
        lineNumbers: false,
        scrollbarStyle: "overlay",
        readOnly: "nocursor",
        completeSingle: false,
        showCursorWhenSelecting: false
    });
}
function sidebarQuery(encrypted, tableName, databaseName) {
    $.ajax({
        type: "POST",
        url: "/sandbox/SidebarQuery",
        dataType: "json",
        data: JSON.stringify({"Encrypted":encrypted, "TableName":tableName, "DatabaseName":databaseName}),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            PopulateDataTables(result);
        },
        error: function (result) {
            console.log(result);
            alert('cant handle request now');
        }
    });
}
function PopulateSideBar(account, ip) {
    $("#insertAfter").empty();
    populateSideBarStatic();
    $.ajax({
        type: "POST",
        url: `/sandbox/PopulateSideBar/${account}?ip=${ip}`,
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                populateDatabase(i, result[i].name);
                for (var x = 0; x < result[i].tables.length; x++) {
                    populateTables(i, x, result[i].tables[x].name);
                    for (var z = 0; z < result[i].tables[x].fields.length; z++) {
                        var tittle = result[i].tables[x].fields[z].name + ' (' + result[i].tables[x].fields[z].type;
                        if (result[i].tables[x].fields[z].data != null && result[i].tables[x].fields[z].data != "") {
                            tittle += ', ' + result[i].tables[x].fields[z].data;
                        }
                        tittle += ' )';
                        populateColumns(i, x, z, tittle, result[i].tables[x].fields[z].type);
                    }
                }
            }
        },
        error: function (result) {
            console.log(result);
            alert('cant Populate SideBar now');
        }
    });
}

function PopulateDataTables(result) {
    console.log(result);
    if (result == null) {
        alert('cant handle that query');
    } else {
        var myNode = document.getElementById("dataTablesZone");
        while (myNode.firstChild) {
            myNode.removeChild(myNode.firstChild);
        }
        myNode.className = "inner-center ui-layout-pane ui-layout-pane-center";
        var response = result.response;
        for (var i = 0; i < response.length; i++) {
            var my_columns = [];
            $.each(response[i].columns, function (key, value) {
                var my_item = {};
                my_item.data = key;
                my_item.title = value;
                my_columns.push(my_item);
            });
            var id = "accountsTable" + i;
            var currentdate = new Date();
            var datetime = "Last Sync: " + currentdate.getDate() + "/"
                + (currentdate.getMonth() + 1) + "/"
                + currentdate.getFullYear() + " "
                + currentdate.getHours() + ":"
                + currentdate.getMinutes() + ":"
                + currentdate.getSeconds();
            $("#dataTablesZone").append('<span>' + datetime + '</span><table id="' + id + '" class="table table-striped table-bordered table-hover responsive w-100"></table>');
            var render = my_columns.length == 2 ? $.fn.dataTable.render.ellipsis(300) : $.fn.dataTable.render.ellipsis(10);
            $('#' + id + '').DataTable({
                data: response[i].data,
                "columns": my_columns,
                columnDefs: [{
                    targets: "_all",
                    render: render
                }],
                "ordering": true,
                "scrollX": true,
                "responsive": true,
                "filter": false,
                "bPaginate": false,
                "bInfo": false
            });

            tooltipAdd();
        }
    }
}

//Edit data table when run query


//Query for sidebar
$(function () {
    $('#sidebar').contextMenu({
        selector: 'ul li a.table-selector',
        items: {
            "edit": {
                name: "Select 1000 Rows",
                icon: "fa-lock",
                callback: function () {
                    var id = $(this)[0].parentNode.id.replace("Tables", "");
                    var bdName = $('#' + id + '').text();
                    var tableName = $(this).text();
                    var encrypted = true;
                    sidebarQuery(encrypted, tableName, bdName);
                }
            },
            "cut": {
                name: "Select 1000 Rows Decrypted",
                icon: "fa-unlock",
                callback: function () {
                    var id = $(this)[0].parentNode.id.replace("Tables", "");
                    var bdName = $('#' + id + '').text();
                    var tableName = $(this).text();
                    var encrypted = false;
                    sidebarQuery(encrypted, tableName, bdName);
                }
            }
        }
    });
});
//Tooltip on body
//$(document).ready(function () {
//    $('[data-toggle="tooltip"]').tooltip({
//        container: 'body'
//    });
//});
//Editor zone

/*"--this is a simple example bbsql\n" +
    "--change the '<change to your database name>' to the database name you want\n" +
    "CREATE DATABASE <change to your database name>;\n" +
    "--here's an example of a simple table creation\n" +
    "CREATE TABLE best_players(position ENCRYPTED RANGE (2, 1, 11) PRIMARY KEY, name ENCRYPTED 4 NOT NULL, !number int);\n" +
    "--on the left sidebar you can learn the Syntax of the language and the available commands\n"*/
var editor = CodeMirror(document.getElementById("codeeditor"), {
    value: "--Select the other tabs if you want to see an example or if you are experienced try it yourself below\n",
    mode: "sql",
    lineNumbers: true,
    extraKeys: { "Ctrl-Space": "autocomplete" },
    scrollbarStyle: "overlay",
    autofocus: true,
    completeSingle: false,
});
// editor.on('inputRead', function onChange(editor, input) {
//     if (input.text[0] === ';' || input.text[0] === ' ') { return; }
// CodeMirror.commands.autocomplete(editor, null, { completeSingle: false })
// });
var lastId = '';
var storeCodeEditorText = [, , ,];
var codeEditor = CodeMirror(document.getElementById("codeeditor"));
function changeCodeEditor(id) {
    console.log(editor);
    var doc = editor.getDoc();
    var queryString = "";
    doc.eachLine(
        function (line) {
            queryString += line.text + "\n";
        }
    );
    if (lastId == 'empty') {
        storeCodeEditorText[0] = queryString;
    }
    else if (lastId == 'hospital') {
        storeCodeEditorText[1] = queryString;

    }
    else if (lastId == 'foodDelivery') {
        storeCodeEditorText[2] = queryString;
    }
    else if (lastId == 'cp') {
        storeCodeEditorText[3] = queryString;
    }
    lastId = id;
    $('a').removeClass('active');
    $('#' + id).addClass('active');

    //Better comments
    document.getElementById("codeeditor").innerHTML = "";
    if (id == 'hospital') {
        if (storeCodeEditorText[1] == null) {
            editor = CodeMirror(document.getElementById("codeeditor"), {
                value: "--this is a example on how to create a simple database that models the business of a hospital with bbsql\n" +
                    "--change the '<change to your database name>' to create the database with the name you want or replace CREATE DATABASE with USE <database Name> to use a database that you have created\n" +
                    "CREATE DATABASE <change to your database name>;\n\n" +

                    "--here's an example of a simple table creation. Some of the records are encrypted. This means they are fully encrypted on the client-side\n" +
                    "--An exclamation mark '!' before a table name means the name of the column is unencrypted. If you want to encrypt the table name remove the ! character\n" +
                    "--Notice the name column, it has the word 'ENCRYPTED' right after it and then the number 4. This means that the data of this column will be encrypted.\n" +
                    "--Furthermore, it also means that encrypted records will be associated to 4 buckets. The more buckets you have the faster you can query data but the more you reveal their frequency.\n" +
                    "--With this configuration you can query records with equality queries '=' and '!=', and bbsql will query records by their buckets instead of the record values themselves.\n" +
                    "--Notice the yearOfBirth column, besides the word 'ENCRYPTED' and the bucket number, it also has 'RANGE(10, 1905, 2999)'.\n" +
                    "--With this configuration you can also make range queries using '<', '<=', '>' and '>=', and it states the range 1905 to 2999 will be devided into 10 consecutive buckets.\n" +
                    "CREATE TABLE staff(!id int PRIMARY KEY , !name ENCRYPTED 4 NOT NULL, !email TEXT, !position TEXT, yearOfBirth ENCRYPTED 4 RANGE(10,1905, 2999) NOT NULL, !address ENCRYPTED 4 NOT NULL, socialSecurity ENCRYPTED 4 NOT NULL, salary ENCRYPTED 5 RANGE(500,1000, 20000));\n" +
                    "--here's an example on how to insert multiple records in the 'staff' table\n" +
                    "--NOTE: you can change and add new records, respect the data types\n" +
                    "INSERT INTO staff(id, name, email, position, yearOfBirth, address,socialSecurity, salary) VALUES (0, 'John','john@bb.com','doctor', 1972 ,'123 John Street','221122334', 16999),\n" +
                    "(1, 'Bill', 'bill@bb.com', 'nurse', 1982, '123 bill Street', '221842314', 6999), \n" +
                    "(2, 'Sarah', 'sarah@bb.com', 'director', 1971, '123 Sarah Street', '221993214', 19500), \n" +
                    "(3, 'Jenniffer', 'jenniffer@bb.com', 'nurse', 1991, '123 jenniffer Street', '234421214', 7500); \n\n" +

                    "--here's another table corresponding to the patients the most sensitive data is encrypted \n" +
                    "CREATE TABLE patients(id int PRIMARY KEY, !name ENCRYPTED 4 NOT NULL, !insurance BOOL, !dateOfAdmission DATETIME);\n" +
                    "--here are some inserts into patients table\n" +
                    "INSERT INTO patients(id , name, insurance, !dateOfAdmission) VALUES (0,'Donald', 'true', 'now()'), \n" +
                    "(1, 'Jair', 'false', 'now()'), \n" +
                    "(2, 'Thommas S.', 'true', 'now()'), \n" +
                    "(3, 'Bruce W.', 'true', 'now()'); \n\n" +

                    "--here's another table corresponding to the visitors\n" +
                    "CREATE TABLE visitors(!id int PRIMARY KEY, name ENCRYPTED 4 NOT NULL, !dateOfVisit DATETIME, !patientVisited int REFERENCES patients(id));\n" +
                    "--populate visitors table with some records \n" +
                    "INSERT INTO visitors (id, name, !dateOfVisit, !patientVisited) VALUES (0,'Kevinho','now()', 3), \n" +
                    "(1, 'Anitta', 'now()', 2), \n" +
                    "(2, 'Caetano', 'now()', 2), \n" +
                    "(3, 'Jorge', 'now()', 3); \n\n" +

                    "--here's an example of a inner join between patients table and visitors table, to find all\n" +
                    "SELECT patients.name, patients.dateOfAdmission, visitors.name, visitors.dateOfVisit FROM patients JOIN visitors ON patients.id = visitors.patientVisited WHERE patients.id = 2;\n" +
                    "--here's an example on how you get data all staff with salary equal or lower than 10000 \n" +
                    "SELECT staff.* FROM staff where staff.salary <= 10000;\n" +
                    "--here you can see the difference between getting data encrypted or decrypted\n" +
                    "SELECT patients.* FROM patients;\n" +
                    "SELECT patients.* FROM  patients ENCRYPTED;\n" +
                    "--this is a simple statment to get all the data, without encryption\n" +
                    "SELECT visitors.* FROM visitors;",
                mode: "sql",
                lineNumbers: true,
                extraKeys: { "Ctrl-Space": "autocomplete" },
                scrollbarStyle: "overlay",
                autofocus: true,
                completeSingle: false,
            });
        } else {
            editor = CodeMirror(document.getElementById("codeeditor"), {
                value: storeCodeEditorText[1],
                mode: "sql",
                lineNumbers: true,
                extraKeys: { "Ctrl-Space": "autocomplete" },
                scrollbarStyle: "overlay",
                autofocus: true,
                completeSingle: false,
            });
        }
    }
    else if (id == 'foodDelivery') {
        if (storeCodeEditorText[2] == null) {
            editor = CodeMirror(document.getElementById("codeeditor"), {
                value: "--this is a example on how to create a database model for an UberEats like business with bbsql\n" +
                    "--change the '<change to your database name>' to create the database with the name you want or replace CREATE DATABASE with USE <database Name> to use a database that you have created\n" +
                    "CREATE DATABASE <change to your database name>;\n\n" +

                    "--here's an example of a simple table creation. All records are encrypted so we can protect clients privacy\n" +
                    "--The value after the reserved word Encrypted is the 'number of buckets' for that column.\n" +
                    "--If the number of buckets is 4, this means all records will fall into 4 buckets and record retrieval will be done by bucket and not by record value.\n" +
                    "--So, the larger the number the quicker it is to retrieve records, but more information regarding the frequency of values is leaked.\n" +
                    "CREATE TABLE clints (!id INT PRIMARY KEY , !name TEXT NOT NULL, email ENCRYPTED 4 NOT NULL, !deliveryAddress ENCRYPTED 4 NOT NULL, billingAddress ENCRYPTED 4 NOT NULL, zipCode TEXT);\n" +
                    "--You can change the name of the table and add, drop or change the name of a column at any time\n" +
                    "--Always be careful changing names adding or dropping columns\n " +
                    "ALTER TABLE clints RENAME TO clients;\n" +
                    "ALTER TABLE clients DROP COLUMN zipCode;\n" +
                    "ALTER TABLE clients ADD COLUMN !zipCode ENCRYPTED 4 NOT NULL;\n" +
                    "--here's an example on how to insert multiple records in the 'clients' table\n" +
                    "--NOTE: you can change and add new records, respect the data types\n" +
                    "INSERT INTO clients(id, name, email, deliveryAddress, billingAddress, zipCode) VALUES (0, 'Mary','mary@bb.com','123 Mary Street','123 Mary Street','85035'),\n" +
                    "(1, 'Bill', 'bill@bb.com', '123 Bill Street', '123 Bill Street', '03217'), \n" +
                    "(2, 'Jack','jack@bb.com','123 Jack Street','123 Jack Street','82941'), \n" +
                    "(3, 'Jenniffer','jenniffer@bb.com','123 Jenniffer Street','123 Jack Street','82941'); \n\n" +

                    "--here's another table corresponding to the restaurants\n" +
                    "CREATE TABLE restaurants(id INT PRIMARY KEY, !restaurantName TEXT, !address TEXT, !areaOfDellivery TEXT, !avgDeliveryTime INT, !priceRange TEXT);\n" +
                    "--here are some inserts into restaurants table\n" +
                    "INSERT INTO restaurants(id, !restaurantName, !address, !areaOfDellivery, !avgDeliveryTime, priceRange) VALUES (0,'Tasca do Careca', 'Rua Ator Taborda 75', 'Lisboa', 30, '$'), \n" +
                    "(1,'Yeatman', 'Rua de Cima 21', 'Vila Nova de Gaia', 60, '$$$$'), \n" +
                    "(2,'Dom Tacho', 'Rua David Sousa 19', 'Lisboa', 45, '$$'), \n" +
                    "(3,'Mad Pizza', 'Praca do Duque de Saldanha ', 'Lisboa', 25, '$$'); \n\n" +

                    "--here's another table corresponding to the distributors\n" +
                    "CREATE TABLE distributors(id INT PRIMARY KEY, name TEXT NOT NULL, !vehicleType TEXT, !rating DECIMAL, phoneNumber ENCRYPTED 4 NOT NULL);\n" +
                    "--populate distributors table with some records \n" +
                    "INSERT INTO distributors (id , name, !vehicleType, rating, phoneNumber) VALUES (0,'Kevinho','Scooter', 3, 914532674), \n" +
                    "(1, 'Mario', 'Bike', 4.2, 928663723), \n" +
                    "(2, 'Julia', 'Car', 5 , 952423723), \n" +
                    "(3,'Quim','Scooter', 4.95, 915542674); \n\n" +

                    "--This table demonstrates how you can correlate multiple tables. In the folowing example we create a table for the oders which needs information from all other tables\n" +
                    "--The REFERENCES keyword is the bridge between two tables. The field in this table references the field in another table\n" +
                    "CREATE TABLE orders(id INT PRIMARY KEY, !client_id int REFERENCES clients(id), !restaurant_id int REFERENCES restaurants(id), !distributor_id int REFERENCES  distributors(id), total DECIMAL);\n" +
                    "INSERT INTO orders (id, !client_id, !restaurant_id, !distributor_id, total) VALUES (0, 1, 0, 3, 12.45),\n" +
                    "(1, 2, 2, 3, 8.12),\n" +
                    "(2, 3, 1, 1, 12.45),\n" +
                    "(3, 3, 1, 2, 12.45);\n\n" +

                    "--If you need to select values from diferent tables you need to use a join connecting the two fiels you want to be the same" +
                    "SELECT clients.name, restaurants.restaurantName, distributors.name, orders.total FROM orders JOIN distributors ON orders.distributor_id = distributors.id JOIN restaurants ON orders.restaurant_id = restaurants.id JOIN clients ON orders.client_id = clients.id;\n" +
                    "--here is how you get all clients without emcryption from clients table \n" +
                    "SELECT clients.* FROM clients;\n" +
                    "--here we compare the same table encrypted information and without encryption \n" +
                    "SELECT restaurants.* FROM restaurants;\nSELECT restaurants.* FROM  restaurants ENCRYPTED;\n" +
                    "SELECT distributors .* FROM distributors  where distributors.rating >= 4.3 ;",
                mode: "sql",
                lineNumbers: true,
                extraKeys: { "Ctrl-Space": "autocomplete" },
                scrollbarStyle: "overlay",
                autofocus: true,
                completeSingle: false,
            });
        } else {
            editor = CodeMirror(document.getElementById("codeeditor"), {
                value: storeCodeEditorText[2],
                mode: "sql",
                lineNumbers: true,
                extraKeys: { "Ctrl-Space": "autocomplete" },
                scrollbarStyle: "overlay",
                autofocus: true,
                completeSingle: false,
            });
        }
    }
    else if (id == 'cp') {
        if (storeCodeEditorText[3] == null) {
            editor = CodeMirror(document.getElementById("codeeditor"), {
                value: "--this is a example on how to create a database model for a Railway System with bbsql\n" +
                    "--change the '<change to your database name>' to create the database with the name you want or replace CREATE DATABASE with USE <database Name> to use a database that you have created\n" +
                    "CREATE DATABASE <change to your database name>;\n\n" +
                    "--here's an example of a simple table creation. Some of the records are encrypted so it's not posible to read the values\n" +
                    "--! before a table name means the name of the column is unencrypted. If you want to encrypt the table name remove the ! character\n" +

                    "CREATE TABLE trains(id INT PRIMARY KEY, !trainCode TEXT, !trainLine TEXT, !startsAt INT, !EndsAt INT);\n" +
                    "INSERT INTO trains (id, !trainCode, !trainLine, !startsAt, !EndsAt) VALUES (0,'2363','Azambuja', 0930, 1600), \n" +
                    "(1,'2262','Sintra', 0900, 1530), \n" +
                    "(2,'2323','Cascais', 1000, 2030), \n" +
                    "(3,'2434','Sado', 1200, 2330); \n\n" +
                    "--If some data needs to be changed and/or is wrong, you can use an Update Statement,\n--by chosing witch column and value after the SET and chosing which row you need to change with the where clause\n" +
                    "UPDATE trains SET trainLine = 'Sintra' where trains.id = 3;\n" +
                    "UPDATE trains SET EndsAt = 2130 where trains.id = 2; \n\n" +

                    "CREATE TABLE staff(id INT PRIMARY KEY , !name ENCRYPTED 4 NOT NULL, !email TEXT, !position TEXT, yearOfBirth ENCRYPTED 4 RANGE(10,1905, 2999) NOT NULL, !address ENCRYPTED 4 NOT NULL, socialSecurity ENCRYPTED 4 NOT NULL, salary ENCRYPTED 5 RANGE(500,1000, 20000), trainId INT REFERENCES trains(id));\n" +
                    "--here's an example on how to insert multiple records in the 'staff' table\n" +
                    "--NOTE: you can change and add new records, respect the data types\n" +
                    "INSERT INTO staff(id, name, email, position, yearOfBirth, address,socialSecurity, salary, trainId) VALUES (0, 'John','john@bb.com','train driver', 1972 ,'123 John Street','221122334', 2000, 1 ),\n" +
                    "(1, 'Bill', 'bill@bb.com', 'train driver', 1982, '123 bill Street', '221842314', 2300, 0), \n" +
                    "(2, 'Sarah', 'sarah@bb.com', 'director', 1971, '123 Sarah Street', '221993214', 19500, null), \n" +
                    "(3, 'Jenniffer', 'jenniffer@bb.com', 'conductor', 1991, '123 Jenniffer Street', '234421214', 7500, 2); \n\n" +

                    "--here's another table corresponding to the stations \n" +
                    "CREATE TABLE stations(id INT PRIMARY KEY, !name ENCRYPTED 4 NOT NULL, !howManyLines INT, !handicapAccess BOOL);\n" +
                    "--here are some inserts into stations table\n" +
                    "INSERT INTO stations(id, !name, !howManyLines, !handicapAccess) VALUES (0,'Santa Apolonia', 15, 'true'), \n" +
                    "(1,'Oriente', 25, 'true'), \n" +
                    "(2,'Sete Rios', 8, 'false'), \n" +
                    "(3,'Faro', 2, 'true'); \n\n" +
                    "--here's another table corresponding to the costumers\n" +

                    "CREATE TABLE costumers(id INT PRIMARY KEY, name ENCRYPTED 4 NOT NULL, !withPass BOOL);\n" +
                    "--populate costumers table with some records \n" +
                    "INSERT INTO costumers (id, name, !withPass) VALUES (0,'50cent','true'), \n" +
                    "(1, 'Clementina Pereira', 'false'), \n" +
                    "(2, 'Shakira Fernanda', 'true'), \n" +
                    "(3, 'Enrique Iglesias', 'false'); \n\n" +

                    "CREATE TABLE trainStation(id INT PRIMARY KEY, train_id INT REFERENCES trains(id), station_id INT REFERENCES stations(id), costumer_id INT REFERENCES costumers(id));\n" +
                    "INSERT INTO trainStation(id , train_id, station_id, costumer_id) VALUES (0,0,0,0),\n" +
                    "(1,0,3,1),\n" +
                    "(2,1,1,2),\n" +
                    "(3,1,2,3);\n" +

                    "--here's an example of a join between two tables \n" +
                    "SELECT costumers.name, trains.trainLine, stations.name, staff.name FROM trainStation JOIN trains ON trainStation.train_id = trains.id JOIN stations ON trainStation.station_id = stations.id JOIN costumers ON trainStation.costumer_id = costumers.id JOIN staff ON trains.id = staff.trainId WHERE trains.trainLine = 'Azambuja';\n" +
                    "--here's an example on how you get data all staff with salary equal or lower than 10000 \n" +
                    "SELECT staff.* FROM staff where staff.salary <= 10000;\n" +
                    "--here you can see the difference between getting data encrypted or decrypted" +

                    "SELECT stations.* FROM stations;\n" +
                    "SELECT stations.* FROM  stations ENCRYPTED;\n" +
                    "--this is a simple statment to get all the data, without encryption" +

                    "SELECT costumers.* FROM costumers;\n" +
                    "SELECT trains.* FROM trains;",
                mode: "sql",
                lineNumbers: true,
                extraKeys: { "Ctrl-Space": "autocomplete" },
                scrollbarStyle: "overlay",
                autofocus: true,
                completeSingle: false,
            });
        }
        else {
            editor = CodeMirror(document.getElementById("codeeditor"), {
                value: storeCodeEditorText[3],
                mode: "sql",
                lineNumbers: true,
                extraKeys: { "Ctrl-Space": "autocomplete" },
                scrollbarStyle: "overlay",
                autofocus: true,
                completeSingle: false,
            });
        }
    }
    else if (id == 'empty') {
        if (storeCodeEditorText[0] == null) {
            editor = CodeMirror(document.getElementById("codeeditor"), {
                value: "--Select the other tabs if you want to see an example or if you are experienced try it yourself below\n",
                mode: "sql",
                lineNumbers: true,
                extraKeys: { "Ctrl-Space": "autocomplete" },
                scrollbarStyle: "overlay",
                autofocus: true,
                completeSingle: false,
            });
        }
        else {
            editor = CodeMirror(document.getElementById("codeeditor"), {
                value: storeCodeEditorText[0],
                mode: "sql",
                lineNumbers: true,
                extraKeys: { "Ctrl-Space": "autocomplete" },
                scrollbarStyle: "overlay",
                autofocus: true,
                completeSingle: false,
            });
        }
    }
}
