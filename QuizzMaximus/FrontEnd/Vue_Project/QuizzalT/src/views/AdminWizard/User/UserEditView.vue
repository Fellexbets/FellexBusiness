<template>
  <div class="container">
    <h4>Edit User</h4>

    <form @submit.prevent="userUpdate()">
      <div class="mb-3">
        <label for="inputUserEditUsername" class="form-label">Current username: {{ userToDisplayCurrent.username }} </label>
        <input v-model="user.username" type="username" class="form-control" id="inputUserEditUsername">
      </div>
      <div class="mb-3">
        <label for="inputUserEditEmail" class="form-label">Current email: {{ userToDisplayCurrent.email }}</label>
        <input v-model="user.email" type="email" class="form-control" id="inputUserEditEmail">
      </div>
      <div class="mb-3">
        <label for="inputUserEditPassword" class="form-label">Current password: {{ userToDisplayCurrent.password }}</label>
        <input v-model="user.password" type="password" class="form-control" id="inputUserEditPassword">
      </div>

      <div class="mb-3">
        <label for="inputUserEditRole" class="form-label">Role</label>
        <select id="inputUserEditRole" v-model="user.role">
          <option value="Admin">Admin</option>
          <option value="Moderator">Moderator</option>
          <option value="Player">Player</option>
        </select>
      </div>

      <button type="submit">Update</button>
      <button @click="back(user.userId)">Back</button>
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
      user: {} as IUser,
      userToDisplayCurrent: {} as IUser
    }
  },
  methods: {
    userUpdate() {
      this.userStore.apiUpdate(this.user);
      this.$router.push(`/userDetails/${this.user.userId}`);
    },
    back(userId: number) {
      this.$router.push(`/userDetails/${userId}`);
    }
  },
  mounted() {
    this.user = this.userStore.getCurrentUser;
    this.userToDisplayCurrent.username = this.user.username;
    this.userToDisplayCurrent.email = this.user.email;
    this.userToDisplayCurrent.password = this.user.password;
    this.userToDisplayCurrent.role = this.user.role;
  },
  computed: {
    ...mapStores(useUserStore)
  }
})
</script>