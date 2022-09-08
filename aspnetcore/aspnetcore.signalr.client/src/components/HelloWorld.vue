<script>
  import {reactive, onMounted} from 'vue'
  import * as signalR from '@microsoft/signalr'
  let connection
  export default {
    name: 'Login',
    setup() {
      const state = reactive({userMessage: '', messages: []})
      const txtMsgOnKeypress = async function(e) {
        if(e.keyCode != 13) return
        await connection.invoke("SendPublicMessage", state.userMessage);
        state.userMessage = ''
      }
      onMounted(async function() {
        connection = new signalR.HubConnectionBuilder()
          .withUrl('https://localhost:7076/hubs/chatroomhub')
          .withAutomaticReconnect()
          .build();
        await connection.start()
        connection.on('ReceivePublicMessage', msg => {
          state.messages.push(msg)
        })
      })
      return {state, txtMsgOnKeypress}
    }
  }
</script>

<template>
  <div>
    <input type="text" v-model="state.userMessage" v-on:keypress="txtMsgOnKeypress"/>
    <div>
      <ul>
        <li v-for="(msg,index) in state.messages" :key="index">
          {{msg}}
        </li>
      </ul>
    </div>
  </div>
  
</template>