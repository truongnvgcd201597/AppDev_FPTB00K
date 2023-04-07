export function getWeatherURL() {
    const URL = 'https://api.openweathermap.org/data/2.5/weather?q=DaNang&appid=1391131b41c67a8425401d03c64b3b39&units=metric&lang=vi';
    return URL;
}

export function getCityName() {
    const cityName = document.querySelector('.city-name');
    return cityName;
}

export function getCurrentTemp() {
    const currentTemp = document.querySelector('.current-temp');
    return currentTemp;
}

export function getImgWeather() {
    const imgWeather = document.querySelector('.img-weather');
    return imgWeather;
}

export function getWeatherImg(){
    const weatherImg = document.querySelector('.img-weather');
    return weatherImg;
}