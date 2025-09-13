import { Component } from '@angular/core';
import { NewYorkTimesService } from '../services/nyt.service';
import { Article } from '../shared/article';
import { ArticleForDb } from '../shared/article4db';
import { ArticleService } from '../services/article.service';

@Component({
  selector: 'app-home',
  templateUrl: './unit3a.component.html'
})
export class Unit3aComponent {

  article!: Article;

  constructor(private nytservice: NewYorkTimesService, private articleservice: ArticleService) { }

  ngOnInit() {

    this.nytservice.getArticles().subscribe(data => {
      this.article = data;

      let articles: ArticleForDb[] = [];

      data.response.docs.forEach(a => {

        let article: ArticleForDb = new ArticleForDb();

        article.abstract = a.abstract;
        article.source = a.source;
        article.image = a.multimedia.thumbnail.url;
        article.imageHeight = a.multimedia.thumbnail.height;
        article.imageWidth = a.multimedia.thumbnail.width;
        article.imageCaption = a.multimedia.caption;

        articles.push(article);
      });

      //this.articleservice.postArticles(articles);
    });
  }
}
