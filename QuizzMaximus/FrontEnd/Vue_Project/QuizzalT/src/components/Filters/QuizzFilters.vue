<template>
<div>

    <label for="search">Search:
        <input class="searchbar " id="search" v-model="term" @keypress.enter="search(term)" />
        <div class="w-100"></div>
    </label>
</div>
</template>


<script lang="ts">
import type { Theme } from "@/models";


export default {
    props: [
        'search'
    ],
    data() {
        return {
            term: ''
        }
    }
}
</script>