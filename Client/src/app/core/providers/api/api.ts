import { inject, Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, } from "@angular/common/http";
import { environment } from "../../../../environments/environment.development";


@Injectable({
    providedIn: "root",
})
export class ApiService {
    private http = inject(HttpClient);
    private basicurl: string = environment.apiURL + "/api";

    getForBasicApi<T>(endpoint: any, queryParamObject?: any, options?: HttpHeaders) {
        if (!options) {
            options = new HttpHeaders({
                "Content-Type": "application/json;charset=utf-8",
            });
            options = options.append("Accept", "application/json");
        }

        let requestURL = `${this.basicurl}/${endpoint}`;
        if (queryParamObject != null && queryParamObject != undefined) {
            let queryParamString = Object.keys(queryParamObject).map(param => param + '=' + queryParamObject[param]).join('&');
            requestURL = requestURL + '/?' + queryParamString;
        }

        return this.http.get<T>(requestURL, {
            headers: options,
            withCredentials: true,
        });
    }

    postForBasicApi<T>(endpoint: any, body: any, options?: HttpHeaders) {
        if (!options) {
            options = new HttpHeaders({
                "Content-Type": "application/json;charset=utf-8",
            });
            options = options.append("Accept", "application/json");
        }
        return this.http.post<T>(`${this.basicurl}/${endpoint}`, body, {
            withCredentials: true
        });
    }

    putForBasicApi<T>(endpoint: any, body: any, options?: HttpHeaders) {
        if (!options) {
            options = new HttpHeaders({
                "Content-Type": "application/json;charset=utf-8",
            });
            options = options.append("Accept", "application/json");
        }
        return this.http.put<T>(`${this.basicurl}/${endpoint}`, body, {
            withCredentials: true
        });
    }

    deleteForBasicApi<T>(endpoint: any, options?: HttpHeaders) {
        if (!options) {
            options = new HttpHeaders({
                "Content-Type": "application/json;charset=utf-8",
            });
            options = options.append("Accept", "application/json");
        }
        return this.http.delete<T>(`${this.basicurl}/${endpoint}`, {
            withCredentials: true
        });
    }
} 
