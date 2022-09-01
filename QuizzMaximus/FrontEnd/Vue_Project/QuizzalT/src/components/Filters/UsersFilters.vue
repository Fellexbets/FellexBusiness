<template>
<div>
<select class="dropdown col-2 " v-model="filter" :key="filter" @change="filterRoles(filter)"> 
        <option v-bind:value= "null_value">Filter by Role</option>
        <option v-for="filter in filters" :key="filter">{{ filter }}</option>

    </select>
    <label for="search">Search:
        <input class="searchbar " id="search" v-model="term" @keypress.enter="search(term)" />
    </label>
</div>
</template>


<script lang="ts">
import type { Theme } from "@/models";

const filters = [
    'All',
    'Admin',
    'Moderator',
    'Player'
    
]

export default {
    props: [
        'filterRoles',
        'search',
        'filter',
        'null_value'
    ],
    data() {
        return {
            filters,
            term: ''
        }
    }
}
</script>