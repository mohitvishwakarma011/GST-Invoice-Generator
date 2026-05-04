export class AppDate{
    public static unixToDate(unixTimeInSeconds:number):Date{
        return new Date(unixTimeInSeconds * 1000);
    }

    public static getCurrentDate():Date{
        return new Date();
    }
}