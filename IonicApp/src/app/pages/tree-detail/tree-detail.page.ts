import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-tree-detail',
  templateUrl: './tree-detail.page.html',
  styleUrls: ['./tree-detail.page.scss'],
})
export class TreeDetailPage implements OnInit {

  tree = null;
  url: string = '';

  constructor(
    private route: ActivatedRoute,
    private api: ApiService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    this.api.getTreeDetails(id).subscribe((res) => {
      this.tree = res;
    });
  }

  openWikipage(tree) {
    if (tree != null) {
      this.url = "https://nl.wikipedia.org/wiki/" + this.tree.name
    }
    window.open(this.url, '_blank');
  }
}
