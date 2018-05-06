import { Component, Input, OnInit } from "@angular/core";
import { Subject } from "rxjs/Subject";

import { SearchService} from "../services";

import { IRequestEngineResult } from "../interfaces";
import { IIssueCategory,   } from "../interfaces";
import { ISearchMusicResult } from "../interfaces/ISearchMusicResult";

@Component({
    selector: "music-search",
    templateUrl: "./musicsearch.component.html",
    styleUrls: ["./../requests/tvrequests.component.scss"],
})
export class MusicSearchComponent implements OnInit {

    public searchText: string;
    public searchChanged = new Subject<string>();
    public musicResults: ISearchMusicResult[];
    public result: IRequestEngineResult;
    public searchApplied = false;

    @Input() public issueCategories: IIssueCategory[];
    @Input() public issuesEnabled: boolean;
    public issuesBarVisible = false;
    public issueRequestTitle: string;
    public issueRequestId: number;
    public issueProviderId: string;
    public issueCategorySelected: IIssueCategory;

    constructor(private searchService: SearchService) {

        this.searchChanged
            .debounceTime(600) // Wait Xms after the last event before emitting last event
            .distinctUntilChanged() // only emit if value is different from previous value
            .subscribe(x => {
                this.searchText = x as string;
                if (this.searchText === "") {
                    this.clearResults();
                    return;
                }
                this.searchService.searchMusic(this.searchText)
                    .subscribe(x => {
                        this.musicResults = x;
                        this.searchApplied = true;
                    });
            });
    }
    public openClosestTab(el: any) {
        el.preventDefault();
        const rowclass = "undefined ng-star-inserted";
        el = el.toElement || el.relatedTarget || el.target;
        while (el.className !== rowclass) {
            // Increment the loop to the parent node until we find the row we need
            el = el.parentNode;
        }
        // At this point, the while loop has stopped and `el` represents the element that has
        // the class you specified

        // Then we loop through the children to find the caret which we want to click
        const caretright = "fa-caret-right";
        const caretdown = "fa-caret-down";
        for (const value of el.children) {
            // the caret from the ui has 2 class selectors depending on if expanded or not
            // we search for both since we want to still toggle the clicking
            if (value.className.includes(caretright) || value.className.includes(caretdown)) {
                // Then we tell JS to click the element even though we hid it from the UI
                value.click();
                //Break from loop since we no longer need to continue looking
                break;
            }
        }
    }

    public ngOnInit() {
        this.searchText = "";
        this.musicResults = [];
        this.result = {
            message: "",
            result: false,
            errorMessage:"",
        };
    }

    public search(text: any) {
        this.searchChanged.next(text.target.value);
    }

    private clearResults() {
        this.musicResults = [];
        this.searchApplied = false;
    }
}