<script setup>
import {ref} from "vue";
import Flight from "@/Models/Flight.js";


const props = defineProps({
  modalActive: Boolean,
  airlines: Array,
  changedItem: Flight
});
const emit = defineEmits(['close'])

let date = new Date();
let offset = Math.abs(date.getTimezoneOffset() / 60);
let newFlightNumber = '';
let newDepartureDate = '';
const newAirline = defineModel({default: ''});
const close = () => {
  emit("close");
};


async function updateData() {
  try {
    let fd = new FormData();
    fd.append("Id", props.changedItem.Id)
    if (newFlightNumber === "") {
      fd.append("FlightNumber", props.changedItem.FlightNumber);
    } else {
      fd.append("FlightNumber", newFlightNumber)
    }

    if (newDepartureDate === "") {
      let formattedOldDepartureDate = props.changedItem.DepartureDate;
      formattedOldDepartureDate = formattedOldDepartureDate.replace("T", " ");
      formattedOldDepartureDate = formattedOldDepartureDate.replace("Z", "");
      fd.append("DepartureDate", formattedOldDepartureDate + "+0");
    } else {
      fd.append("DepartureDate", newDepartureDate + "+" + offset);
    }

    if (newAirline.value === "") {
      fd.append("AirlineId", props.changedItem.Airline.Id);
    } else {
      console.log(newAirline)
      fd.append("AirlineId", newAirline.value);
    }

    await fetch('http://localhost:5214/Flights', {
      method: "PUT",
      body: fd
    })
    close();
  } catch (error) {
    console.log(error);
  }
}


//   return {newDepartureDate, newFlightNumber, newAirline, close, updateData};
// },
</script>

<template>
  <transition name="modal-animation">
    <div v-show="modalActive" class="modal">
      <transition name="modal-animation-inner">
        <div v-show="modalActive" class="modal-inner">
          <div class="container">

            <div class="column">
              <h1>текущие значения</h1>
              <p>номер рейса</p>
              <p>{{ changedItem.FlightNumber }}</p>
              <p>Время вылета</p>
              <p>{{ changedItem.DepartureDate }}</p>
              <p>Авиакомпания</p>
              <p>{{ changedItem.Airline.FullName }}</p>
            </div>

            <div class="column">
              <h1>Новые значения</h1>
              <p>номер рейса</p>
              <input type="text" minlength="1" maxlength="10" size="10" v-model="newFlightNumber"/>
              <p>Время вылета</p>
              <input type="datetime-local" v-model="newDepartureDate">
              <p>Авиакомпания</p>
              <select v-model="newAirline">
                <option disabled value="">Выберите</option>
                <option v-for="item in airlines" v-bind:value="item.Id">{{ item.FullName }}</option>
              </select>
            </div>

          </div>
          <slot/>
          <button @click="close" type="button">Закрыть</button>
          <button @click="updateData" type="button">Подтвердить</button>
        </div>
      </transition>
    </div>
  </transition>
</template>

<style scoped>
.container {
  display: flex;
}

/* Setting the width of each column to 50% */
.column {
  width: 50%;
}

.modal {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  width: 100vw;
  position: fixed;
  top: 0;
  left: 0;
  background-color: rgba(90, 50, 117, 0.7);

  .modal-inner {
    position: relative;
    max-width: 480px;
    width: 80%;
    box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06);
    background-color: #29374c;
    padding: 64px 16px;

    i {
      position: absolute;
      top: 15px;
      right: 15px;
      font-size: 20px;
      cursor: pointer;

      &:hover {
        color: crimson;
      }
    }
  }
}
</style>