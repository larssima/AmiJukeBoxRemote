import {inject} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import {AppSettings} from '../infrastructure/app-settings';

@inject(HttpClient, AppSettings, )
export class BaseRepo {
    constructor(http, appSettings, ) {
        http.configure(config => {
            config
                .withDefaults({
                    credentials: 'include', //Oklart om denna har nån funktion Valid values; omit, same-origin and include
                    headers: {
                        //'Content-Type': 'application/json',
                        'Accept': 'application/json'
                    }
                })
                .withInterceptor({
                    request(request) {
                        console.log(`Requesting ${request.method} ${request.url}`);
                        return request; // you can return a modified Request, or you can short-circuit the request by returning a Response
                    },
                    response(response) {
                        console.log(`Received ${response.status} ${response.url}`);
                        return response; // you can return a modified Response
                    }
                })
        });

        this.http = http;
        this.baseUrl = appSettings.api;
    }

    get(url) {
        console.log('BaseRepo(get): ' + url);
        return this.http.fetch(this.baseUrl + url)
            .then(response => { return response.json(); })
            .then(data => { return data; });
    }

    put(url, data) {
        console.log('BaseRepo(put): ' + url, data);
        return this.http.fetch(this.baseUrl + url, {
            method: 'put',
            body: json(data)
        })
            .then(response => response.json())
            .then(data => { return data; });
    }

    post(url, data) {
        console.log('BaseRepo(post): ' + url, data);
        return this.http.fetch(this.baseUrl + url, {
            method: 'post',
            body: json(data)
        })
            .then(response => response.json())
            .then(data => { return data; });
    }

    delete(url) {
        console.log('BaseRepo(delete): ' + url);
        return this.http.fetch(this.baseUrl + url, { method: 'delete' })
            .then(response => response.json())
            .then(data => { return data; });
    }
}