const URL = 'https://api.openweathermap.org/data/2.5/weather?q=DaNang&appid=1391131b41c67a8425401d03c64b3b39&units=metric&lang=vi';
const cityName = document.querySelector('.city-name');
const currentTemp = document.querySelector('.current-temp');
const imgWeather = document.querySelector('.img-weather');

const currentTime = document.querySelector('.current-time');

async function GetWeatherStatus() {
    const respone = await fetch(URL);
    const data = await respone.json();
    console.log(data)
    cityName.textContent = data.name || DEFAULT_VALUE;
    currentTemp.textContent = data.main.temp || DEFAULT_VALUE;
    imgWeather.src = `http://openweathermap.org/img/w/${data.weather[0].icon}.png`;

}

function TimeCurrent() {
    const now = new Date();
    const hours = now.getHours();
    const mintunes = now.getMinutes();
    const seconds = now.getSeconds().toString().padStart(2, 0);

    currentTime.textContent = `${hours}:${mintunes}:${seconds}`;
}


(() => {
    GetWeatherStatus();
    setInterval(() => {
        TimeCurrent();
    }, 1000)
})()