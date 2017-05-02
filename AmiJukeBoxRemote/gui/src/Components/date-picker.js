import {inject, customElement, bindable} from 'aurelia-framework';
import moment from 'moment';
import $ from 'jquery';
import {datepicker} from 'eonasdan/bootstrap-datetimepicker';

@inject(Element)
export class DatePicker {
    @bindable format = "YYYY-MM-DD";
    @bindable value;

    constructor(element) {
        this.element = element;
    }

    attached() {
        this.datePicker = $(this.element)
            .find('.input-group.date')
            .datetimepicker({
                format: this.format,
                showClose: true,
                showTodayButton: true,
                sideBySide : true,
                showClear: true
            });

        this.datePicker.on("dp.change", (e) => {
            if(e.date === false)
                this.value = null;
            else
                this.value = moment(e.date).format(this.format);
        });
    }
}