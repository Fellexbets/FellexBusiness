<template>
    <header>

        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark border-bottom box-shadow mb-3">
            <div class="container">

                <RouterLink class="navbar-brand" to="/">Home</RouterLink>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse"
                    aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>

                <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                    <ul class="navbar-nav flex-grow-1">
                        <li v-if="!authStore.user" class="nav-item">
                            <RouterLink to="/login">Login</RouterLink>
                            <RouterLink to="/signup">Signup</RouterLink>
                        </li>
                        <li v-else class="nav-item">
                            <RouterLink to="/profile">Profile</RouterLink>
                            <RouterLink to="/gamealt">Play a Quizz</RouterLink>
                            <RouterLink v-if="authStore.checkIfAdmin" to="/dashboard">Dashboard</RouterLink>
                            <RouterLink to="/" @click="authStore.logout()">Logout</RouterLink>
                        </li>
                    </ul>
                </div>

            </div>
        </nav>
    </header>

</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useAuthStore } from "@/stores";
import { mapStores } from "pinia";

export default defineComponent({

    computed: {
        ...mapStores(useAuthStore)
    },

})
</script>


<style>
</style>
