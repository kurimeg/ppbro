<template>
  <div>
    <app-header />
    <v-content>
      <slot />
    </v-content>
  </div>
</template>

<script>
import AppHeader from '@/components/globals/AppHeader'

export default {
  components: {
    AppHeader
  }
}
</script>