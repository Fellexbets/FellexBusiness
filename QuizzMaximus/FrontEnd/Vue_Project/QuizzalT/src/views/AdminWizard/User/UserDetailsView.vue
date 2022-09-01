<template>
  <div class="container">
    <h4>User Details</h4>
    <table class="table">
      <thead>
        <tr>
          <th>Id</th>
          <th>Username</th>
          <th>Email</th>
          <th>Role</th>
          <th>Date Created</th>
          <th>Password</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td> {{ user.userId }} </td>
          <td> {{ user.username }} </td>
          <td> {{ user.email }} </td>
          <td> {{ user.role }} </td>
          <td> {{ user.dateCreated }} </td>
          <td> {{ user.password }} </td>
        </tr>
      </tbody>
    </table>
    <button @click="userEdit(user)">Edit</button>
    <button @click="userDelete(user)">Delete</button>
    <button @click="back()">Back</button>
  </div>
  <br />
  <br />

  <div class="container">
    <h4>Bffs</h4>
    <table class="table">
      <thead>
        <tr>
          <th>Id</th>
          <th>Username</th>
          <th>Email</th>
          <th>Role</th>
          <th>Date Created</th>
          <th>Password</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in userBffs" :key="item.userId">
          <td> {{ item.userId }} </td>
          <td> {{ item.username }} </td>
          <td> {{ item.email }} </td>
          <td> {{ item.role }} </td>
          <td> {{ item.dateCreated }} </td>
          <td> {{ item.password }} </td>
        </tr>
      </tbody>
    </table>
  </div>

  <div class="container">
    <h4>Friends</h4>
    <table class="table">
      <thead>
        <tr>
          <th>Id</th>
          <th>Username</th>
          <th>Email</th>
          <th>Role</th>
          <th>Date Created</th>
          <th>Password</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in userFriends" :key="item.userId">
          <td> {{ item.userId }} </td>
          <td> {{ item.username }} </td>
          <td> {{ item.email }} </td>
          <td> {{ item.role }} </td>
          <td> {{ item.dateCreated }} </td>
          <td> {{ item.password }} </td>
        </tr>
      </tbody>
    </table>
  </div>

  <div class="container">
    <h4>WatchList</h4>
    <table class="table">
      <thead>
        <tr>
          <th>Id</th>
          <th>Username</th>
          <th>Email</th>
          <th>Role</th>
          <th>Date Created</th>
          <th>Password</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in userWatchList" :key="item.userId">
          <td> {{ item.userId }} </td>
          <td> {{ item.username }} </td>
          <td> {{ item.email }} </td>
          <td> {{ item.role }} </td>
          <td> {{ item.dateCreated }} </td>
          <td> {{ item.password }} </td>
        </tr>
      </tbody>
    </table>
  </div>

  <div class="container">
    <h4>Blocked Users</h4>
    <table class="table">
      <thead>
        <tr>
          <th>Id</th>
          <th>Username</th>
          <th>Email</th>
          <th>Role</th>
          <th>Date Created</th>
          <th>Password</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in userWatchList" :key="item.userId">
          <td> {{ item.userId }} </td>
          <td> {{ item.username }} </td>
          <td> {{ item.email }} </td>
          <td> {{ item.role }} </td>
          <td> {{ item.dateCreated }} </td>
          <td> {{ item.password }} </td>
        </tr>
      </tbody>
    </table>
  </div>

</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useUserStore, useUserRelationStore } from "@/stores/";
import type { IUser } from "@/models/";

export default defineComponent({
  data() {
    return {
      user: {} as IUser,
      userBffs: [] as IUser[],
      userFriends: [] as IUser[],
      userWatchList: [] as IUser[],
      userBlocked: [] as IUser[],
    }
  },
  methods: {
    userEdit(user: IUser) {
      this.userStore.setCurrentUser(user);
      this.$router.push(`/userEdit/${user.userId}`)
    },
    userDelete(user: IUser) {
      this.userStore.setCurrentUser(user);
      this.$router.push(`/userDelete/${user.userId}`)
    },
    back() {
      this.$router.push(`/userIndex`);
    }
  },
  mounted() {
    this.user = this.userStore.getCurrentUser;
    const userBffRelations     = this.userRelationStore.getFilteredUserRelations(this.user.userId, 1);
    const userFriendRelations  = this.userRelationStore.getFilteredUserRelations(this.user.userId, 2);
    const userWatchRelations   = this.userRelationStore.getFilteredUserRelations(this.user.userId, 4);
    const userBlockedRelations = this.userRelationStore.getFilteredUserRelations(this.user.userId, 4);

    this.userBffs      = this.userStore.getUsers.filter( c => userBffRelations.find(u => u.relatedUserId == c.userId))
    this.userFriends   = this.userStore.getUsers.filter( c => userFriendRelations.find(u => u.relatedUserId == c.userId))
    this.userWatchList = this.userStore.getUsers.filter( c => userWatchRelations.find(u => u.relatedUserId == c.userId))
    this.userBlocked   = this.userStore.getUsers.filter( c => userBlockedRelations.find(u => u.relatedUserId == c.userId))
  },
  computed: {
    ...mapStores(useUserStore, useUserRelationStore)
  }
})
</script>

