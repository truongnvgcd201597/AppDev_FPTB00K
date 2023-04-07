import { getWeatherURL, getCityName, getCurrentTemp, getWeatherImg  } from "./selectors.js";

async function WeatherStatus() {
   const URL = getWeatherURL();
    const respone = await fetch(URL);
    const data = await respone.json();
    console.log(data);
    getCityName().textContent = data.name || DEFAULT_VALUE;
    getCurrentTemp().textContent = data.main.temp || DEFAULT_VALUE;
    getWeatherImg.src = `http://openweathermap.org/img/w/${data.weather[0].icon}.png`;
}


(() => {
    WeatherStatus();
})()