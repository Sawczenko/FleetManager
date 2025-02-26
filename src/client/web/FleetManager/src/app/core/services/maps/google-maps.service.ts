import {Injectable} from '@angular/core';
import {ApiService} from '../api/api.service';
import {Result, ValueResult} from '../api/models/result';
import {Loader} from '@googlemaps/js-api-loader';

@Injectable({
  providedIn: 'root'
})
export class GoogleMapsService {

  private googleMapsLoaded = false;
  private loader?: Loader;

  constructor(private apiService: ApiService) {
  }

  loadGoogleMaps(): Promise<void> {
    return new Promise((resolve, reject) => {
      if (this.googleMapsLoaded) {
        resolve();
        return;
      }

      this.apiService.get<ValueResult<string>>('/Configuration/googleMapsApiKey').subscribe({
        next: response => {
          if(this.googleMapsLoaded) {
            return;
          }

          this.loader = new Loader({apiKey: response.value, version: "weekly"});
          this.loader
            .importLibrary('maps')
            .then(() => {
              this.googleMapsLoaded = true;
              console.log("Google Maps loaded");
            }).catch(error => {
            console.log(error);
          })
        },
        error: error => {
          reject(error);
        }
      })
    })

  }

  unloadGoogleMaps() {
    if (!this.googleMapsLoaded) return;

    // const scripts = document.querySelectorAll('script[src*="maps.googleapis.com"]');
    // scripts.forEach(script => script.remove());
    //
    // delete (window as any).google;


    if (this.loader) {
      this.loader.deleteScript();
    }

    this.googleMapsLoaded = false;

    console.log('Google Maps API unloaded.');
  }
}
