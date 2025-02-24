export function initializeDataTable({
    tableId,
    apiUrl,
    method,
    columns,
    ordering = true,
    additionalParameters = null,
    failureCallback = null
}) {
    if ($.fn.DataTable.isDataTable(tableId)) {
        // If DataTable already exists, return its instance
        return $(tableId).DataTable();
    }
    return $(tableId).DataTable({
        processing: true, // Show processing indicator
        serverSide: true, // Enable server-side processing
        ordering: ordering !== false,
        ajax: {
            url: apiUrl, // API endpoint
            type: "POST",
            contentType: "application/json",
            data: function (d) {
                const parameters = [d]; // Default DataTables request object
                if (typeof additionalParameters === "function") {
                    parameters.push(...additionalParameters());
                }
                else {
                    if (additionalParameters) {
                        parameters.push(...additionalParameters); // Append additional parameters if provided
                    }   
                }
                return JSON.stringify({
                    Method: method, // Method to call in the backend
                    Parameters: [...parameters] // Sending the entire DataTables request object as the first parameter
                });
            },
            dataSrc: function (json) {
                return json.data; // Path to the array in your API response
            },
            dataFilter: function (jsonString) {
                const json = JSON.parse(jsonString);
                if (!json.isSuccess) {
                    // Call the failure callback function if it's provided
                    if (failureCallback) {
                        failureCallback(json.error);
                    } else {
                        console.warn('You Mast To Add CallBack In Data Table Initialize');
                        console.error(json.error);
                    }
                    return JSON.stringify({
                        draw: 1,
                        recordsTotal: 0,
                        recordsFiltered: 0,
                        data: []
                    });
                }

                // Extract the necessary fields for DataTables
                return JSON.stringify({
                    draw: json.data.draw,
                    recordsTotal: json.data.recordsTotal,
                    recordsFiltered: json.data.recordsFiltered,
                    data: json.data.data
                });
            }
        },
        columns: columns,
        lengthMenu: [5,1, 10, 25, 50, 100],
        language: {
            processing: '<div class="spinner-border text-primary" role="status"></div>',
            paginate: {
                //previous: `<button class='btn btn-sm btn-primary'>Previous</button>`,
                //next: `<button class='btn btn-sm btn-primary'>Next</button>`,
            },
            search: "Filter records:",
        },

    });
}



export function initializeDataTableAjax({
    tableId,
    apiUrl,
    columns,
    ordering = true,
    additionalParameters = null,
    failureCallback = null
}) {
    if ($.fn.DataTable.isDataTable(tableId)) {
        // If DataTable already exists, return its instance
        return $(tableId).DataTable();
    }
    return $(tableId).DataTable({
        processing: true,      // Show processing indicator
        serverSide: true,      // Enable server-side processing
        ordering: ordering !== false,
        ajax: {
            url: apiUrl,     // New API endpoint URL
            type: "POST",
            contentType: "application/json",
            data: function (d) {
                // Create base payload structure expected by the backend
                if (additionalParameters) {
                    // Create base payload structure
                    const payload = {
                        DataTablesRequest: {
                            draw: d.draw,
                            start: d.start,
                            length: d.length,
                            search: d.search,
                            order: d.order,
                            columns: d.columns
                        }
                    };

                    // Merge additional parameters directly into the root object
                    if (additionalParameters) {
                        const extraParams = typeof additionalParameters === "function" ?
                            additionalParameters() :
                            additionalParameters;

                        // Spread the additional parameters into the root payload
                        Object.assign(payload, extraParams);
                    }

                    return JSON.stringify(payload);
                }
                else {
                    return JSON.stringify(d);
                }
            },
            dataSrc: function (json) {
                // Return the data array from the BaseResponse data property
                return json.data;
            },
            dataFilter: function (jsonString) {
                const json = JSON.parse(jsonString);
                if (!json.isSuccess) {
                    // If the response indicates failure, call the callback (if provided) or log a warning.
                    if (failureCallback) {
                        failureCallback(json.error);
                    } else {
                        console.warn('You must add a callback in DataTable initialization');
                        console.error(json.error);
                    }
                    // Return an empty DataTables response
                    return JSON.stringify({
                        draw: 1,
                        recordsTotal: 0,
                        recordsFiltered: 0,
                        data: []
                    });
                }
                // Return only the necessary fields for DataTables consumption.
                return JSON.stringify({
                    draw: json.data.draw,
                    recordsTotal: json.data.recordsTotal,
                    recordsFiltered: json.data.recordsFiltered,
                    data: json.data.data
                });
            }
        },
        columns: columns,
        lengthMenu: [5, 1, 10, 25, 50, 100],
        language: {
            processing: '<div class="spinner-border text-primary" role="status"></div>',
            paginate: {
                // Customize pagination buttons if desired
            },
            search: "Filter records:",
        },
    });
}

export function reinitializeDataTable({
    tableId,
    apiUrl,
    method,
    columns,
    additionalParameters = null,
    failureCallback = null
}) {
    // 1. Check if DataTable is initialized and destroy it if needed
    if ($.fn.DataTable.isDataTable(tableId)) {
        $(tableId).DataTable().clear().destroy(); // Destroy the existing DataTable instance
    }

    // 2. Reinitialize the DataTable
    return initializeDataTable({
        tableId: tableId,
        apiUrl: apiUrl,
        method: method,
        columns: columns,
        additionalParameters: additionalParameters,
        failureCallback: failureCallback
    });
}