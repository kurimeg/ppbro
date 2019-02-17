<template>
  <v-timeline-item
    color="accent"
  >
    <v-layout row wrap pt-3>
      <v-flex xs12 md3>
        <strong>{{ certificate.issueDate }}</strong>
      </v-flex>
      <v-flex xs12 md9>
        {{ certificate.issuerName }}<br/>
        {{ certificate.detail + ' ' + certificate.type }}
        <div class="caption"></div>
      </v-flex>
    </v-layout>
  </v-timeline-item>
</template>

<script>
export default {
  props: {
    certificate: Object
  }
}
</script>
