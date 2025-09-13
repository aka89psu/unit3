export interface Article {
  response: Response;
}

export interface Response {
  docs: Document[];
}

export interface Document {
  abstract: string;
  document_type: string;
  section_name: string;
  snippet: string;
  source: string;
  type_of_material: string;
  web_url: string;

  byline: ByLine;
  headline: Headline;
  multimedia: Multimedia
}

export interface ByLine {
  original: string;
}

export interface Headline {
  main: string;
}

export interface Multimedia {
  caption: string;
  credit: string;
  default: MultimediaDetail;
  thumbnail: MultimediaDetail;
}

export interface MultimediaDetail {
  url: string;
  height: number;
  width: number;
}

