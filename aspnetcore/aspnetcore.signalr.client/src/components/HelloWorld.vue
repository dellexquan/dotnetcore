<script>
  import {reactive, onMounted} from 'vue'
  import * as signalR from '@microsoft/signalr'
  import axios  from 'axios'
  let connection
  export default {
    name: 'Login',
    setup() {
      const state = reactive({username: '', password: '', toUserName: '', privateMessage: '', userMessage: '',  messages: []})
      const login = async function() {
        const credential = {
          userName: state.username,
          password: state.password
        }
        axios.post('https://localhost:7076/api/demo/login2', credential)
        .then(async res=>{
          const token = res.data
          connectSignalR(token)
        })
        .catch(err=>{
          console.log(err)
        })
      }
      const txtMsgOnKeypress = async function(e) {
        if(e.keyCode != 13) return
        await connection.invoke("SendPublicMessage", state.userMessage);
        state.userMessage = ''
      }
      const txtPrivateMsgOnKeyPress = async function(e) {
        if(e.keyCode != 13) return
        await connection.invoke("SendPrivateMessage", state.toUserName, state.privateMessage)
        state.privateMessage = ''
      }

      const connectSignalR = async function(token) {
        console.log(token)
        //const options = { skipNegotiation: true, transport: signalR.HttpTransportType.WebSockets}
        const options = { accessTokenFactory: () => token }
        connection = new signalR.HubConnectionBuilder()
          //.withUrl('https://localhost:7076/hubs/chatroomhub')
          .withUrl('https://localhost:7076/hubs/chatroomhub', options)
          //.withAutomaticReconnect()
          .build();
        await connection.start()
        connection.on('ReceivePublicMessage', msg => {
          state.messages.push(msg)
        })
        connection.on('ReceivePrivateMessage', (from, msg) => {
          state.messages.push(from + ': ' + msg)
        })
      }

      onMounted(async function() {
        
      })

      return {state, login, txtMsgOnKeypress, txtPrivateMsgOnKeyPress, connectSignalR}
    }
  }
</script>

<template>
  <div>
    <div>
      Public: <br>
      Message: <input type="text" v-model="state.userMessage" v-on:keypress="txtMsgOnKeypress"/>
    </div>
    <div>
      Private: <br>
      To User Name: 
      <input type="text" v-model="state.toUserName" /><br>
      Message: <br>
      <input type="text" v-model="state.privateMessage" v-on:keypress="txtPrivateMsgOnKeyPress" />
    </div>
    <div>
      <div>
        User Name: <input type="text" v-model="state.username" />
        Password: <input type="password" v-model="state.password" />
        <button v-on:click="login">Login</button>
      </div>
      <ul>
        <li v-for="(msg,index) in state.messages" :key="index">
          {{msg}}
        </li>
      </ul>
    </div>
  </div>
  
</template>