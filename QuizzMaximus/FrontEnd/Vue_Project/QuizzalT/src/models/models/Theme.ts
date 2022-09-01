export interface ITheme {
    "themeId": number;
    "themeName": string;
}

export class Theme {
    constructor(data?: ITheme){
        Object.assign(this, data);
    }
}