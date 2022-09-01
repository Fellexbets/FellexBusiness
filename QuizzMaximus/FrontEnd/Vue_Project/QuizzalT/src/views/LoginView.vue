<template>
  <NavBarComponent></NavBarComponent>
  <div id="loginPage">
    <div class="container w-50 p-3 boxShadow" id="loginDiv">
      <h1 class="text-center">Log in</h1>
  
      <form @submit.prevent="submit()">
  
        <label for="inputLoginEmail" class="form-label">Email address</label>
        <input v-model="user.email" type="email" class="form-control" id="inputLoginEmail">
        <label for="inputLoginPassword" class="form-label">Password</label>
        <input v-model="user.password" type="password" class="form-control" id="inputLoginPassword">
        <br />
        <button class="w-100" type="submit">Login</button> <p>{{ errorinfo }}</p>
        <button class="w-100" @click="$router.push('/signup')">Sign Up</button>
        <br />
      </form>
  
    </div>
  
   <!-- <div class="container w-50 p-3">
      <div class="row">
        <div class="col text-center">
          <button class="w-50" @click="adminBypass()">adminBypass</button>
        </div>
        <div class="col text-center">
          <button class="w-50" @click="igorBypass()">igorBypass</button>
        </div>
      </div>
    </div> -->
    <br /><br /><br /><br />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useAuthStore, useUserStore } from "@/stores/";
import type { IUser } from "@/models";

export default defineComponent({
  data() {
    return {
      user: {} as IUser,
      errorinfo: ''
    }
  },
  methods: {
    submit() {
      this.authStore.authenticate(this.user);
    },
    adminBypass() {
      this.authStore.authenticate({ userId: 1, username: "Professor", email: "Professor@email.com", role: "Admin", password: "123qwerty" } as IUser);
    },
    igorBypass() {
      this.authStore.authenticate({ userId: 2, username: "Igor", email: "Igor@email.com", role: "Moderator", password: "123qwerty" } as IUser);
    },
  },
  computed: {
    ...mapStores(useAuthStore, useUserStore)
  }
})
</script>