import { initializeDataTable } from '../../../utility/dataTableUtility.js';
import { setupModalData } from '../../../utility/dataModalUtility.js';
import { makeMessageSeenAsync } from '../../../services/ticketMessageService.js';

let ticketMessageTable;

$(document).ready(function () {
    let ticketId = $('#ticketId').val();


    ticketMessageTable = initializeDataTable({
        tableId: '#ticketMessageTable',
        apiUrl: 'https://localhost:7264/ticketMessage/call',
        method: 'getallticketmessagespaginatedasync',
        columns: [
            { data: 'id', name: 'ID' },
            //{ data: 'content', name: 'Message' },
            { data: 'stageAtTimeOfMessage', name: 'StageAtTimeOfMessageStageAtTimeOfMessage' },
            {
                data: 'createdAt',
                name: 'SentAt',
                render: function (data, type, row) {
                    return new Date(data).toLocaleString();
                }
            },
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
                    //remove this after adding the permission
                    canDelete = true;
                    canEdit = true;
                    canView = true;
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
        additionalParameters: [ticketId],
        failureCallback: showErrorMessage
    });

    // Row click event handler
    ticketMessageTable.on('click', '.dt-view', async function () {
        let data = ticketMessageTable.row($(this).parents('tr')).data();
        // getAllNotSeenMessageForTicketAsync and get the count of not seen messages
        let notSeenMessageResult = await makeMessageSeenAsync(data.id);
        try {
            if (!notSeenMessageResult.isSuccess) 
                showErrorModal(updateResult.error.description);
        } catch (e) {
            showErrorModal('There is an error that happened!');
        }

        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        const title = 'Project Details';
        const bodyContent = `
             <div class="form-group">
                <label for="name">ID :</label>
                <input type="text" class="form-control" id="id-view" name="id" value="${data.id}" disabled>
            </div>
            <div class="form-group">
                <label for="content">Message :</label>
                <textarea class="form-control" id="content" name="content" disabled>${data.content}</textarea>
            </div>
            <div class="form-group">
                <label for="createdAt">Sent At :</label>
                <input type="text" class="form-control" id="createdAt" name="createdAt" value="${new Date(data.createdAt).toLocaleString()}" disabled>
            </div>
        `;
        const viewButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [viewButton]);
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
export { ticketMessageTable };
