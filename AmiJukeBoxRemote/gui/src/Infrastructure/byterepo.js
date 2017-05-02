import {inject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {AppSettings} from '../infrastructure/app-settings';

@inject(HttpClient, AppSettings)
export class ByteRepo {
    constructor(http, appSettings) {
        http.configure(config => {
            //config.withBaseUrl(appSettings.api);
        });

        this.http = http;
        this.baseUrl = appSettings.api;
    }

    get(url) {
        console.log('BaseRepo(get): ' + url);

        return this.http.fetch(this.baseUrl + url)
            .then(data => { return data; });
    }

    //Files must be of type 'FileList' - (aurelia model bind file) - https://www.danyow.net/binding-to-file-inputs-with-aurelia/
    post(url, data, files) {
        var form = new FormData();

        //append data to formdata - data[key] = value, key = property name
        if (data != null) {
            for (let key of Object.keys(data)) {
                form.append(data[key], key);
            }
        }

        //append files to formdata
        for(let i = 0; i < files.length; i++) {
            form.append('file', files.item(i));
        }

        return this.http.fetch(this.baseUrl + url, {
            method: "post",
            body    : form
        }).then(response => response.json())
        .then(data => { return data; });
    }
}