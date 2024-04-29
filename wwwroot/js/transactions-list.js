"use strict";$(document).ready(function(){let e,t,o;o=(isDarkStyle?(e=config.colors_dark.borderColor,t=config.colors_dark.bodyBg,config.colors_dark):(e=config.colors.borderColor,t=config.colors.bodyBg,config.colors)).headingColor;var s=document.querySelectorAll(".toast");s&&s.forEach(function(e){new bootstrap.Toast(e).show()}),$("#transactionsTable").DataTable({order:[[1,"desc"]],displayLength:7,dom:'<"mx-4 d-flex flex-wrap flex-column flex-sm-row gap-2 py-4 py-sm-0"<"d-flex align-items-center me-auto"l><"dt-action-buttons text-xl-end text-lg-start text-md-end text-start d-flex flex-sm-row align-items-center justify-content-md-end gap-2 ms-n2 ms-md-2 flex-wrap flex-sm-nowrap"fB>>t<"row mx-4"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6 pb-3 ps-0"p>>',lengthMenu:[7,10,15,20],language:{searchPlaceholder:"Search..",search:"",lengthMenu:"_MENU_"},buttons:[{extend:"collection",className:"btn btn-label-secondary dropdown-toggle ms-2 me-0 me-md-3 mx-sm-3 waves-effect waves-light",text:'<i class="ti ti-download me-1"></i>Export',buttons:[{extend:"print",title:"Transactions Data",text:'<i class="ti ti-printer me-2" ></i>Print',className:"dropdown-item",customize:function(e){$(e.document.body).css("color",config.colors.headingColor).css("border-color",config.colors.borderColor).css("background-color",config.colors.body),$(e.document.body).find("table").addClass("compact").css("color","inherit").css("border-color","inherit").css("background-color","inherit"),$(e.document.body).find("h1").css("text-align","center")},exportOptions:{columns:[1,2,3,4,5,6]}},{extend:"csv",title:"Transactions",text:'<i class="ti ti-file me-2" ></i>Csv',className:"dropdown-item",exportOptions:{columns:[1,2,3,4,5,6]}},{extend:"excel",title:"Transactions",text:'<i class="ti ti-file-spreadsheet me-2"></i>Excel',className:"dropdown-item",exportOptions:{columns:[1,2,3,4,5,6]}},{extend:"pdf",title:"Transactions",text:'<i class="ti ti-file-text me-2"></i>Pdf',className:"dropdown-item",exportOptions:{columns:[1,2,3,4,5,6]}},{extend:"copy",title:"Transactions",text:'<i class="ti ti-copy me-2" ></i>Copy',className:"dropdown-item",exportOptions:{columns:[1,2,3,4,5,6]}}]},{text:'<i class="ti ti-plus me-0 me-sm-1 ti-xs"></i><span class="d-none d-md-inline-block">Add Transaction</span>',className:"add-new btn btn-primary waves-effect waves-light",action:function(e,t,o,s){window.location.href="/Transactions/Add"}}],responsive:!0,rowReorder:{selector:"td:nth-child(2)"},columnDefs:[{className:"control",searchable:!1,orderable:!1,responsivePriority:2,targets:0,render:function(e,t,o,s){return""}},{targets:1,responsivePriority:4},{targets:2,responsivePriority:3},{targets:3,responsivePriority:9},{targets:4,responsivePriority:5},{targets:5,responsivePriority:7},{targets:6,responsivePriority:8},{targets:-1,searchable:!1,orderable:!1,responsivePriority:1}],responsive:{details:{display:$.fn.dataTable.Responsive.display.modal({header:function(e){return"Details of "+e.data()[2]}}),type:"column",renderer:function(e,t,o){var s=$.map(o,function(e,t){return t<o.length-1&&""!==e.title?'<tr data-dt-row="'+e.rowIndex+'" data-dt-column="'+e.columnIndex+'"><td>'+e.title+":</td> <td>"+e.data+"</td></tr>":""}).join("");return!!s&&$('<table class="table mt-3"/><tbody />').append(s)}}}})}),setTimeout(()=>{$(".dataTables_filter .form-control").removeClass("form-control-sm"),$(".dataTables_length .form-select").removeClass("form-select-sm"),$(".dt-buttons").addClass("d-flex align-items-center gap-3 gap-md-0"),$("#transactionsTable_length").addClass("mt-0 mt-md-3")},300);