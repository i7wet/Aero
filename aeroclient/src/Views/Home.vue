<script setup>
import EditingModal from "@/components/Modal/EditingModal.vue";
import {ref} from 'vue'
import Flight from "@/Models/Flight.js";
import Airline from "@/Models/Airline.js";

let loading = ref(true);
let flights = ref([]);
let airlines = ref([]);
let date = new Date();
let offset = Math.abs(date.getTimezoneOffset() / 60);

let newFlight = new Flight('', '', '', new Airline('', '', ''))
let selectedItemForChange = ref(new Flight('', '', '', new Airline('', '', '')));
const modalActive = ref(false);

const toggleModal = () => {
  modalActive.value = !modalActive.value;
};

function takeItemForChanged(item) {
  console.log(item);
  selectedItemForChange.value = item;
  toggleModal();
}

async function sendData() {
  try {
    let fd = new FormData();
    fd.append("FlightNumber", newFlight.FlightNumber)
    fd.append("DepartureDate", newFlight.DepartureDate + "+" + offset)
    fd.append("AirlineId", newFlight.Airline.Id)
    await fetch('http://localhost:5214/Flights', {
      method: "POST",
      body: fd
    })
  } catch (error) {
    console.log(error);
  }
  await takeFlightsData();
}

function convertJsonFlights(json) {
  let array = [];
  for (let item of json) {
    let airline = new Airline(item.airline.id, item.airline.fullName, item.airline.iATACode)
    let flight = new Flight(item.id, item.flightNumber, item.departureDate, airline);
    array.push(flight)
  }
  return array;
}

async function takeAirlinesData() {
  try {
    let airlinesResponse = await fetch('http://localhost:5214/Airlines');
    airlines.value = await airlinesResponse.json();
  } catch (error) {
    console.log(error)
  }
}

async function takeFlightsData() {
  try {
    let flightsResponse = await fetch('http://localhost:5214/Flights');
    flights.value = convertJsonFlights(await flightsResponse.json());
  } catch (error) {
    console.log(error)
  }
}

async function loadData() {
  await takeAirlinesData()
  await takeFlightsData()
  loading.value = false;
}

loadData();
</script>

<template>
  <template v-if="loading">
    Загружается
  </template>
  <template v-else></template>
  <fieldset>
    <p>номер рейса</p>
    <input type="text" required minlength="1" maxlength="10" size="10" v-model="newFlight.FlightNumber"/>
    <p>Время вылета</p>
    <input type="datetime-local" v-model="newFlight.DepartureDate">
    <p>Авиакомпания</p>
    <select v-model="newFlight.Airline.Id">
      <option disabled value="">Выберите</option>
      <option v-for="airline in airlines" v-bind:value="airline.id">{{ airline.fullName }}</option>
    </select>
    <button @click="sendData()" type="button" style="float: right">Добавить</button>
  </fieldset>
  <table>
    <thead>
    <tr>
      <th>Номер рейса</th>
      <th>Дата вылета</th>
      <th>Авиакомпания</th>
      <th>Кнопка</th>
    </tr>
    </thead>
    <tbody>
    <tr v-for="flight in flights">
      <td>{{ flight.FlightNumber }}</td>
      <td>{{ flight.DepartureDate }}</td>
      <td>{{ flight.Airline.FullName }}</td>
      <td>
        <button @click="takeItemForChanged(flight)" type="button">Изменить</button>
      </td>
    </tr>
    </tbody>
  </table>
  <EditingModal @close="toggleModal" :modalActive="modalActive" :airlines="airlines"
                :changedItem="selectedItemForChange">
    <div class="modal-content">

    </div>
  </EditingModal>

</template>

<style scoped>
table {
  font-family: arial, sans-serif;
  border-collapse: collapse;
  width: 100%;
}

td, th {
  border: 1px solid #372a2a;
  text-align: center;
  padding: 8px;
}

tr:nth-child(even) {
  background-color: #504848;
}

.modal-content {
  display: flex;
  flex-direction: column;
}
</style>