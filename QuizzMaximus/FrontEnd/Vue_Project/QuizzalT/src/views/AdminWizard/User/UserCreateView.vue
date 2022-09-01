<template>
  <div class="container">
    <h4>Create User</h4>

    <form @submit.prevent="userCreate()">
      <div class="mb-3">
        <label for="inputUserCreateUsername" class="form-label">Username</label>
        <input v-model="user.username" type="username" class="form-control" id="inputUserCreateUsername">
      </div>
      <div class="mb-3">
        <label for="inputUserCreateEmail" class="form-label">Email</label>
        <input v-model="user.email" type="email" class="form-control" id="inputUserCreateEmail">
      </div>
      <div class="mb-3">
        <label for="inputUserCreatePassword" class="form-label">Password</label>
        <input v-model="user.password" type="password" class="form-control" id="inputUserCreatePassword">
      </div>
      <div class="mb-3">
        <label for="inputUserCreateRole" class="form-label">Role</label>
        <select id="inputUserCreateRole" v-model="user.role">
          <option value="Admin">Admin</option>
          <option value="Moderator">Moderator</option>
          <option value="Player">Player</option>
        </select>
      </div>

      <button type="submit">Create</button>
      <button @click="back()">Back</button>
    </form>

  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useUserStore } from "@/stores/";
import type { IUser } from "@/models/";

export default defineComponent({
  data() {
    return {
      user: {} as IUser
    }
  },
  methods: {
    userCreate() {
      this.userStore.apiCreate(this.user);
      //this.$router.push(`/userDetails/${this.user.userId}`);
      this.$router.push(`/userIndex`);
    },
    back() {
      this.$router.push(`/userIndex`);
    }
  },
  computed: {
    ...mapStores(useUserStore)
  }
})
</script>