﻿import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

    constructor(private _httpService: Http) { }

    apiValues: string[] = [];
    ngOnInit() {
        
    }
}
