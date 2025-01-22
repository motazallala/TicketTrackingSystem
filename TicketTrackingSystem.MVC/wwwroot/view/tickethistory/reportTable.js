import { initializeDataTable } from '../../utility/dataTableUtility.js';
import { setupModalData } from '../../utility/dataModalUtility.js';
import { getDeliveryStatusDropdown } from '../../services/ticketHistoryService.js';
import { getStageDropdown } from '../../services/projectServices.js';

let reportTable;
$(document).ready(async function () {
    let stageDropdown = $('#stageFilter');
    let deliveryStatusDropdown = $('#deliveryStatusFilter');
    let StageResult = await getStageDropdown();
    let DeliveryStatusResult = await getDeliveryStatusDropdown();
    if (!StageResult.isSuccess) {
        showErrorMessage(StageResult.error.description);
    }
    else {
        stageDropdown.append(StageResult.data);
    }
    if (!DeliveryStatusResult.isSuccess) {
        showErrorMessage(DeliveryStatusResult.error);
    }
    else {
        deliveryStatusDropdown.append(DeliveryStatusResult.data);
    }
    // Initialize with empty filters
    let currentFilters = {
        stage: '',
        deliveryStatus: ''
    };

    // Function to reload table with filters
    function reloadTableWithFilters() {
        if ($.fn.DataTable.isDataTable('#reportTable')) {
            reportTable.ajax.reload();
        }
    }
    reportTable = initializeDataTable({
        tableId: '#reportTable',
        apiUrl: 'https://localhost:7264/ticketHistory/call',
        method: 'getalltickethistoryforreportasync',
        columns: [
            { data: 'ticketId', name: 'TicketId' },
            { data: 'title', name: 'Title' },
            { data: 'deliveryStatusFrom', name: 'DeliveryStatusFrom' },
            { data: 'assignedFrom', name: 'AssignedFrom' },
            { data: 'stageFrom', name: 'StageFrom' },
            { data: 'deliveryStatusTo', name: 'deliveryStatusTo' },
            { data: 'assignedTo', name: 'AssignedTo' },
            { data: 'stageTo', name: 'StageTo' },
            {
                data: "id",
                name: 'Actions',
                orderable: false,
                searchable: false,
                render: function (data, type, row) {
                    // Retrieve the values from the hidden inputs
                    var canView = $('#canView').val() === "true";
                    var canEdit = $('#canEdit').val() === "true";
                    var canDelete = $('#canDelete').val() === "true";

                    // Start building the action buttons HTML
                    var actionButtons = `<div class="d-flex justify-content-center">`;

                    // Add the 'Details' button if viewing is allowed
                    if (canView) {
                        actionButtons += `
                            <button class="btn btn-primary btn-sm me-2 dt-view">
                                <i class="bi bi-info-square-fill"></i> Details
                            </button>`;
                    }

                    // Close the div and return the HTML
                    actionButtons += `</div>`;

                    return actionButtons;
                }
            }
        ],
        additionalParameters: () => [ // Wrap in function
            
                 currentFilters.stage,
                 currentFilters.deliveryStatus
            
        ], 
        failureCallback: showErrorMessage
    });


    // Add event listeners for dropdown changes
    $('#stageFilter, #deliveryStatusFilter').on('change', function () {
        currentFilters = {
            stage: $('#stageFilter').val(),
            deliveryStatus: $('#deliveryStatusFilter').val()
        };
        reloadTableWithFilters();
    });

    // Row click event handler
    reportTable.on('click', '.dt-view', function () {
        let data = reportTable.row($(this).parents('tr')).data();
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        const title = 'Role Details';
        const bodyContent = `
             <div class="form-group">
                <label for="name">ID :</label>
                <input type="text" class="form-control" id="id-view" name="id" value="${data.id}" disabled>
            </div>
            <div class="form-group">
                <label for="name">Name :</label>
                <input type="text" class="form-control" id="name-view" name="name" value="${data.name}" disabled>
            </div>
        `;
        const viewButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        const moreDetails = `<a class="btn btn-sm btn-primary dt-view" href="/Role/Details/${data.id}">View</a>`;
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [viewButton, moreDetails]);
        $('#myModal').modal('show');
    });

    // Function to handle errors
    function showErrorMessage(error) {
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');
        const title = `Error ${error.code}`;
        const bodyContent = `<p>${error.description}</p>`;
        const closeButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [closeButton]);
        $('#myModal').modal('show');
    }
    $('#myModal').on('hidden.bs.modal', function (evt) {
        $('.modal .modal-title').empty();
        $('.modal .modal-body').empty();
        $('.modal .modal-footer').empty();
    });
});

// Export the DataTable instance when it's ready
export { reportTable };
