import { inject, bindable, customElement, containerless } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { MapJukeboxService } from '../services/mapjukeboxservice';
import { DialogService } from 'aurelia-dialog';
import { ConfirmDialog } from '../Components/confirmation-dialog';
import * as toastr from 'toastr';


@inject(MapJukeboxService, EventAggregator, DialogService, ConfirmDialog)

@containerless()

export class MapEditSelections {

  constructor(mapJukeboxService, eventAggregator, dialogService, confirmDialog) {
    this.mapJukeboxService = mapJukeboxService;
    this.dialogService = dialogService;
    this.ea = eventAggregator;
    this.mapJukeboxSelections = [];
    this.mapJukeboxSelection = '';
    this.confirmDialog = confirmDialog;
    this.value = 'Are you sure?';
    this.insert_JbLetter = '';
    this.insert_JbNumberA = '';
    this.insert_JbNumberB = '';
    this.insert_A1Song = '';
    this.insert_A2Song = '';
    this.insert_B1Song = '';
    this.insert_B2Song = '';
    this.insert_Artist1 = '';
    this.insert_Artist2 = '';
    this.insert_ImageStripTemplate = '';
    this.insert_MusicCategory = '';
    this.Insert_Archived = 0;
    this.canSave = false;
  }

  activate()
  {
    // return this.mapIdpsActivitiesService.getAllRmActivities().then(data => {
    //   this.rmActivities = data;
    //   this.mapIdpsActivitiesService.getAllIdpsActivities().then(data => {
    //     this.idpsActivities = data;
    //   })
    // });
  }

  attached() {
    return this.loadData();
  }

  loadData() {
    return this.mapJukeboxService.getAllJukeboxSelections().then(data => {
      this.mapJukeboxSelections = data;
      });
  }


  deleteRmIdpsAct(activity) {
    // var _this = this;
    // this.dialogService.open({ viewModel: ConfirmDialog, model: { value: "Are you sure you want to delete activity: " + activity.RmActivityCode + " (" + activity.IdpsActivityCode +  ") ?" } }).then(function (response) {
    //   if (!response.wasCancelled) {
    //     _this.mapIdpsActivitiesService.deleteRmIdpsActivity(activity).then((activity) => {
    //       _this.loadData()
    //     });
    //   }
    // });
  }

  insertSelection(selection) {
    console.log(selection);
    // if (this.insert_IdpsActivityCode=='' || this.insert_RmActivityCode=='') return;
    // this.mapIdpsActivitiesService.insertRmIdpsActivity(this.insert_RmActivityCode,this.insert_RmDescription,this.insert_IdpsActivityCode,this.insert_Description,this.insert_IdpsDescription).then((request) => {
    //   this.loadData()
    //   this.mapIdpsActivity = '';
    //   this.insert_RmActivityCode = '';
    //   this.insert_RmDescription = '';
    //   this.insert_IdpsActivityCode = '';
    //   this.insert_Description = '';
    //   this.insert_IdpsDescription = '';
    //   $('#new-activity-modal').modal('hide');
    // });
  }


}
